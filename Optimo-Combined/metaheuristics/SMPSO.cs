//
//  SMPSO.cs
//
//  Author:
//       Antonio J. Nebro <antonio@lcc.uma.es>
//
//  Copyright (c) 2011 Antonio J. Nebro
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.operators.comparator;
//using jmetal.util;
//using jmetal.util.archives;

namespace Optimo_Combined
{
  internal class _SMPSO : Algorithm
  {

    public int swarmSize_ { get; set; }
    public int archiveSize_ { get; set; }
    public int maxIterations_ { get; set; }
    public int iteration_ { get; set; }
    public SolutionSet particles_ { get; set; }
    public Solution[] best_ { get; set; }

    public CrowdingArchive leaders_;

    public double[,] speed_;

    public IComparer dominance_ = new CrowdingDistanceComparator ();
    public IComparer crowdingDistanceComparator_;

    // private Distance distance_;

    public Operator polynomialMutation_;

    public double r1Max_;
    public double r1Min_;
    public double r2Max_;
    public double r2Min_;
    public double C1Max_;
    public double C1Min_;
    public double C2Max_;
    public double C2Min_;
    public double WMax_;
    public double WMin_;
    public double ChVel1_;
    public double ChVel2_;

    public double[] deltaMax_;
    public double[] deltaMin_;

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="problem">
    /// A <see cref="Problem"/>
    /// </param>
    public _SMPSO (Problem problem) : base(problem)
    {
      r1Max_ = 1.0;
      r1Min_ = 0.0;
      r2Max_ = 1.0;
      r2Min_ = 0.0;
      C1Max_ = 2.5;
      C1Min_ = 1.5;
      C2Max_ = 2.5;
      C2Min_ = 1.5;
      WMax_ = 0.1;
      WMin_ = 0.1;
      ChVel1_ = -1;
      ChVel2_ = -1;
    }

    public _SMPSO()
    {
        // TODO: Complete member initialization
    }

    /// <summary>
    /// Initializing parameters
    /// </summary>
    private void initializeParameters ()
    {
      swarmSize_ = (int)inputParameters_["swarmSize"];
      archiveSize_ = (int)inputParameters_["archiveSize"];
      maxIterations_ = (int)inputParameters_["maxIterations"];

      //Console.WriteLine ("SwarmSize: " + swarmSize_);
      //Console.WriteLine ("Archive Size: " + archiveSize_);
      //Console.WriteLine ("maxIterations: " + maxIterations_) ;
      polynomialMutation_ = operators_["mutation"];
      
      iteration_ = 0;

      particles_ = new SolutionSet (swarmSize_);
      best_ = new Solution[swarmSize_];
      leaders_ = new CrowdingArchive (archiveSize_, problem_.numberOfObjectives_);
      
      // Create comparators for dominance and crowding distance
      dominance_ = new DominanceComparator ();
      crowdingDistanceComparator_ = new CrowdingDistanceComparator ();

      // Create the speed_ vector
      speed_ = new double[swarmSize_, problem_.numberOfVariables_];
      
      deltaMax_ = new double[problem_.numberOfVariables_];
      deltaMin_ = new double[problem_.numberOfVariables_];
      for (int i = 0; i < problem_.numberOfVariables_; i++) {
        deltaMax_[i] = (problem_.upperLimit_[i] - problem_.lowerLimit_[i]) / 2.0;
        deltaMin_[i] = -deltaMax_[i];
        
    }
      // for
    }
    // initParams
    /// <summary>
    /// Adapting inertia Weight
    /// </summary>
    /// <param name="iter">
    /// A <see cref="System.Int32"/>
    /// </param>
    /// <param name="miter">
    /// A <see cref="System.Int32"/>
    /// </param>
    /// <param name="wma">
    /// A <see cref="System.Double"/>
    /// </param>
    /// <param name="wmin">
    /// A <see cref="System.Double"/>
    /// </param>
    /// <returns>
    /// A <see cref="System.Double"/>
    /// </returns> Adaptive inertia
    private double inertiaWeight (int iter, int miter, double wma, double wmin)
    {
      return wma;
      // - (((wma-wmin)*(double)iter)/(double)miter);
    }

    /// <summary>
    /// Constriction Coefficient
    /// </summary>
    /// <param name="c1">
    /// A <see cref="System.Double"/>
    /// </param>
    /// <param name="c2">
    /// A <see cref="System.Double"/>
    /// </param>
    /// <returns>
    /// A <see cref="System.Double"/>
    /// </returns>
    private double constrictionCoefficient (double c1, double c2)
    {
      double rho = c1 + c2;
      //rho = 1.0 ;
      if (rho <= 4) {
        return 1.0;
      } else {
        return 2 / (2 - rho - Math.Sqrt (Math.Pow (rho, 2.0) - 4.0 * rho));
      }
    }
    // constrictionCoefficient
    /// <summary>
    /// Velocity constriction
    /// </summary>
    /// <param name="v">
    /// A <see cref="System.Double"/>
    /// </param>
    /// <param name="deltaMax">
    /// A <see cref="System.Double[]"/>
    /// </param>
    /// <param name="deltaMin">
    /// A <see cref="System.Double[]"/>
    /// </param>
    /// <param name="variableIndex">
    /// A <see cref="System.Int32"/>
    /// </param>
    /// <param name="particleIndex">
    /// A <see cref="System.Int32"/>
    /// </param>
    /// <returns>
    /// A <see cref="System.Double"/>
    /// </returns>
    public double velocityConstriction (double v, double[] deltaMax, double[] deltaMin, int variableIndex, int particleIndex)
    {
      double result;
      
      double dmax = deltaMax[variableIndex];
      double dmin = deltaMin[variableIndex];
      
      result = v;
      
      if (v > dmax) {
        result = dmax;
      }
      
      if (v < dmin) {
        result = dmin;
      }
      
      return result;
    }

    /// <summary>
    /// Compute de speed of the particles of the swarm
    /// </summary>
    /// <param name="iter">
    /// A <see cref="System.Int32"/>
    /// </param>
    /// <param name="miter">
    /// A <see cref="System.Int32"/>
    /// </param>
    public void computeSpeed (int iter, int miter)
    {
      double r1, r2, W, C1, C2;
      double wmax, wmin, deltaMax, deltaMin;
      Solution bestGlobal;

      for (int i = 0; i < swarmSize_; i++) {
        Solution particle = particles_[i];
        Solution bestParticle = best_[i];
        
        //Select a global best_ for calculate the speed of particle i, bestGlobal
        Solution one, two;
        int pos1 = PseudoRandom.Instance ().Next (0, leaders_.size () - 1);
        int pos2 = PseudoRandom.Instance ().Next (0, leaders_.size () - 1);
        one = leaders_[pos1];
        two = leaders_[pos2];
        
        if (crowdingDistanceComparator_.Compare (one, two) < 1) {
          bestGlobal = one;
        } else {
          bestGlobal = two;
          //Params for velocity equation
        }
        r1 = r1Min_ + PseudoRandom.Instance ().NextDouble () * (r1Max_ - r1Min_);
        r2 = r2Min_ + PseudoRandom.Instance ().NextDouble () * (r2Max_ - r2Min_);
        C1 = C1Min_ + PseudoRandom.Instance ().NextDouble () * (C1Max_ - C1Min_);
        C2 = C2Min_ + PseudoRandom.Instance ().NextDouble () * (C2Max_ - C2Min_);
        W = WMin_ + PseudoRandom.Instance ().NextDouble () * (WMax_ - WMin_);
        //
        wmax = WMax_;
        wmin = WMin_;
        
        for (int v = 0; v < particle.numberOfVariables_; v++) {
          //Computing the velocity of this particle

          speed_[i, v] = velocityConstriction (constrictionCoefficient (C1, C2) *
            (inertiaWeight (iter, miter, wmax, wmin) *
              speed_[i, v] + C1 * r1 * (bestParticle.variable_[v].value_ - particle.variable_[v].value_) +
              C2 * r2 * (bestGlobal.variable_[v].value_ - particle.variable_[v].value_)), deltaMax_, deltaMin_, v, i);
        }
      }
    }
    // computeSpeed
    /// <summary>
    ///Compute tne new positions of the particles
    /// </summary>
    public void computeNewPositions ()
    {
      for (int i = 0; i < swarmSize_; i++) {
        Solution particle = particles_[i];
        for (int v = 0; v < particle.numberOfVariables_; v++) {
          particle.variable_[v].value_ += speed_[i, v];
          
          if (particle.variable_[v].value_ < problem_.lowerLimit_[v]) {
            particle.variable_[v].value_ = problem_.lowerLimit_[v];
            speed_[i, v] = speed_[i, v] * ChVel1_;
            //
          }
          if (particle.variable_[v].value_ > problem_.upperLimit_[v]) {
            particle.variable_[v].value_ = problem_.upperLimit_[v];
            speed_[i, v] = speed_[i, v] * ChVel2_;
            //
          }
        }
      }
    }


    public void mopsoMutation (int actualIteration, int totalIterations)
    {
      for (int i = 0; i < particles_.size (); i++) {
        // We apply mutation to the 6% of the particles
        if ((i % 6) == 0)
          polynomialMutation_.execute (particles_[i]);
        //if (i % 3 == 0) { //particles_ mutated with a non-uniform mutation %3
        //  nonUniformMutation_.execute(particles_[i]);
        //} else if (i % 3 == 1) { //particles_ mutated with a uniform mutation operator
        //  uniformMutation_.execute(particles_[i]);
        //} else //particles_ without mutation
        //;
      }
    }
    // mopsoMutation
    /// <summary>
    /// Run the algorithm
    /// </summary>
    /// <returns>
    /// A <see cref="SolutionSet"/>
    /// </returns>
    public override SolutionSet execute ()
    {
      // Step 1. Initlialize parameters
      initializeParameters ();

      // Step 2. Initialize the swarm
      for (int i = 0; i < swarmSize_; i++) {
        Solution particle = new Solution (problem_);
        problem_.evaluate (particle);
        problem_.evaluateConstraints (particle);
        particles_.add (particle);
      }
      
      // Step 3. Initialize the speed_ of each particle to 0
      for (int i = 0; i < swarmSize_; i++) {
        for (int j = 0; j < problem_.numberOfVariables_; j++) {
          speed_[i, j] = 0.0;
        }
      }

      // Step 4. Initialize leader archive
      for (int i = 0; i < particles_.size(); i++) {
        Solution particle = new Solution (particles_[i]);
        leaders_.add (particle);
      }
      
      // Step 5. Initialize the memory of each particle
      for (int i = 0; i < particles_.size (); i++) {
        Solution particle = new Solution (particles_[i]);
        best_[i] = particle;
      }
      
      // Step 6. Assign the crowding distance to the leaders
      Distance.crowdingDistanceAssignment (leaders_, problem_.numberOfObjectives_);

      iteration_ = 0;
      // Step 7. Main loop
      int evals = 0 ;
      while (iteration_ < maxIterations_) {
        //Compute the speed_
        computeSpeed (iteration_, maxIterations_);
        
        //Compute the new positions for the particles_
        computeNewPositions ();
        
        //Mutate the particles_          
        mopsoMutation (iteration_, maxIterations_);
        
        //Evaluate the new particles_ in new positions
        for (int i = 0; i < particles_.size (); i++) {
          Solution particle = particles_[i];
          problem_.evaluate (particle);
        }
        
        //Actualize the archive          
        for (int i = 0; i < particles_.size (); i++) {
          Solution particle = new Solution (particles_[i]);
          leaders_.Add (particle);
        }
        
        //Actualize the memory of this particle
        for (int i = 0; i < particles_.size (); i++) {
          int flag = dominance_.Compare (particles_[i], best_[i]);
          if (flag != 1) {
            // the new particle is best_ than the older remeber        
            Solution particle = new Solution (particles_[i]);
            best_[i] = particle;
          }
        }
        
        //Assign the crowding distance to the leaders_
        Distance.crowdingDistanceAssignment (leaders_, problem_.numberOfObjectives_);
        iteration_++;
      }
      return leaders_;
    }
  }
}
