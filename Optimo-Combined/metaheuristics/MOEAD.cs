//
//  MOEAD.cs
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
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.operators.comparator;
//using jmetal.util;

namespace Optimo_Combined
{
  internal class MOEAD : Algorithm
  {
    public int populationSize_;
    public int maxEvaluations_;
    public SolutionSet population_;
    public double[] z_;
    // Ideal point
    public double[][] lambda_;
    // Lambda vectors
    public int T_;
    // Neighbourhood size
    public int[][] neighborhood_;
    public double delta_;
    public int nr_;
    public string dataDirectory_;

    public Solution[] indArray_;
    public string functionType_;

    public Operator crossover_;
    public Operator mutation_;

    public int evaluations_;

    public MOEAD (Problem problem) : base(problem)
    {
      functionType_ = "_TCHE1";
    }

    // MOEAD
    public override SolutionSet execute ()
    {
      populationSize_ = (int)inputParameters_["populationSize"];
      maxEvaluations_ = (int)inputParameters_["maxEvaluations"];
      dataDirectory_ = (string)inputParameters_["dataDirectory"];
      
      //System.Console.WriteLine ("Solves Name:" + problem_.problemName_);
      //System.Console.WriteLine ("Pop size : " + populationSize_);
      //System.Console.WriteLine ("Max Evals: " + maxEvaluations_);
      //System.Console.WriteLine ("Data directory: " + dataDirectory_);
      
      // Reading operators
      mutation_ = operators["mutation"];
      crossover_ = operators["crossover"];
      //selectionOperator = operators["selection"];
      
      //System.Console.WriteLine ("Crossover parameters: " + crossover_);
      //System.Console.WriteLine ("Mutation parameters: " + mutation_);
      
      // Initializing variables
      population_ = new SolutionSet (populationSize_);
      evaluations_ = 0;
      indArray_ = new Solution[problem_.numberOfObjectives_];

      T_ = (int)(populationSize_);    //changed from 0.1*populationSize_
      // 20;
      delta_ = 0.9;
      nr_ = (int)(populationSize_);  //changed from 0.1*populationSize_
      // 2 ;
      neighborhood_ = new int[populationSize_][];
      for (int i = 0; i < populationSize_; i++)
        neighborhood_[i] = new int[T_];
      
      
      z_ = new double[problem_.numberOfObjectives_];
      lambda_ = new double[populationSize_][];
      for (int i = 0; i < populationSize_; i++)
        lambda_[i] = new double[problem_.numberOfObjectives_];

      // STEP 1. Initialization
      // STEP 1.1. Compute euclidean distances between weight vectors and find T
      initUniformWeight ();

      // STEP 1.2. Init neighborhoods
      initNeighborhood();

      // STEP 1.3. Init population
      initPopulation ();

      // STEP 1.4. Init ideal point
      initIdealPoint ();

      // STEP 2. Update
      do {
        int[] permutation = new int[populationSize_];
        RandomPermutation.execute (permutation, populationSize_);
        //Console.WriteLine ("evalations: " + evaluations_);

        for (int i = 0; i < populationSize_; i++) {
          
          
          //int n = permutation[i]; // or int n = i;
          int n = i;
          // or int n = i;
          int type;
          double rnd = PseudoRandom.Instance ().NextDouble ();
          
          // STEP 2.1. Mating selection based on probability
          // if (rnd < realb)    
          if (rnd < delta_) {
            type = 1;
            // neighborhood
          } else {
            type = 2;
            // whole population
          }

          List<int> p = new List<int> ();
          matingSelection (p, n, 2, type);
          
          Solution child;
          Solution[] parents = new Solution[3];
          
          parents[0] = population_[p[0]];
          parents[1] = population_[p[1]];
          parents[2] = population_[n];

          // Apply DE crossover 
          child = (Solution)crossover_.execute (new Object[] { population_[n], parents });

          // Apply mutation
          mutation_.execute (child);

          // Evaluation
          problem_.evaluate (child);
          
          evaluations_++;

          // STEP 2.4. Update z_
          updateReference (child);

          // STEP 2.5. Update of solutions
          updateProblem (child, n, type);
        }
      } while (evaluations_ < maxEvaluations_);
      return population_;
    }
    // execute

    /// <summary>
    ///
    /// </summary>
    public void initUniformWeight ()
    {
      if ((problem_.numberOfObjectives_ == 2) && (populationSize_ < 300)) {
        for (int n = 0; n < populationSize_; n++) {
          double a = 1.0 * n / (populationSize_ - 1);
          lambda_[n][0] = a;
          lambda_[n][1] = 1 - a;
        }
        // for
        // if
      } else {
        string dataFileName;
        dataFileName = dataDirectory_ + "W" + problem_.numberOfObjectives_ + "D_" + populationSize_ + ".dat";
        TextReader tr = new StreamReader (dataFileName);
        
        int i = 0;
        int j = 0;
        
        string sr = tr.ReadLine ();
        while (sr != null) {
          j = 0;
          
          char[] charSeparators = new char[] { ' ' };
          
          string[] vals = sr.Split (charSeparators, StringSplitOptions.RemoveEmptyEntries);
          
          foreach (string v in vals) {
            lambda_[i][j] = double.Parse (v);
            j++;
          }
          i++;
          sr = tr.ReadLine ();
        }
        
      }
      // else
    }

    private void initPopulation ()
    {
      for (int i = 0; i < populationSize_; i++) {
        Solution newSolution = new Solution (problem_);
        
        problem_.evaluate (newSolution);
        evaluations_++;
        population_.@add (newSolution);
      }
      // for
    }
    // initPopulation

    public void initIdealPoint()
    {
      for (int i = 0; i < problem_.numberOfObjectives_; i++) {
        z_[i] = 1.0e+30;
        indArray_[i] = new Solution (problem_);
        //problem_.evaluate (indArray_[i]);
        evaluations_++;
      }
      // for
      for (int i = 0; i < populationSize_; i++) {
        updateReference (population_[i]);
      }
      // for
    }
    // initIdealPoint

    public void updateReference(Solution individual)
    {
      for (int n = 0; n < problem_.numberOfObjectives_; n++) {
        if (individual.objective_[n] < z_[n]) {
          z_[n] = individual.objective_[n];
          
          indArray_[n] = individual;
        }
      }
    }
    // updateReference


    public void initNeighborhood ()
    {
      double[] x = new double[populationSize_];
      int[] idx = new int[populationSize_];
      
      for (int i = 0; i < populationSize_; i++) {
        // calculate the distances based on weight vectors
        for (int j = 0; j < populationSize_; j++) {
            x[j] = Optimo_Combined.Distance.distVector(lambda_[i], lambda_[j]);
          //x[j] = dist_vector(population[i].namda,population[j].namda);
          idx[j] = j;
          //System.out.println("x["+j+"]: "+x[j]+ ". idx["+j+"]: "+idx[j]) ;
        }
        // for
        // find 'niche' nearest neighboring subproblems
        Optimo_Combined.MinFastSort.execute(x, idx, populationSize_, T_);
        //minfastsort(x,idx,population.size(),niche);
        
        for (int k = 0; k < T_; k++) {
          neighborhood_[i][k] = idx[k];
          //System.out.println("neg["+i+","+k+"]: "+ neighborhood_[i][k]) ;
        }
      }
      // for
   } // initNeighborhood


    public void matingSelection (List<int> list, int cid, int size, int type)
    {
      // list : the set of the indexes of selected mating parents
      // cid  : the id of current subproblem
      // size : the number of selected mating parents
      // type : 1 - neighborhood; otherwise - whole population
      int ss;
      int r;
      int p;
      
      ss = neighborhood_[cid].Length;
      while (list.Count < size) {
        if (type == 1) {
          r = PseudoRandom.Instance ().Next (0, ss - 1);
          p = neighborhood_[cid][r];
          //p = population[cid].table[r];
        } else {
          p = PseudoRandom.Instance ().Next (0, populationSize_ - 1);
        }
        bool flag = true;
        for (int i = 0; i < list.Count; i++) {
          // p is in the list
          if (list[i] == p) {
            flag = false;
            break;
          }
        }
        
        //if (flag) list.push_back(p);
        if (flag) {
          list.Add (p);
        }
      }
    }
    // matingSelection



    public void updateProblem (Solution indiv, int id, int type)
    {
      // indiv: child solution
      // id:   the id of current subproblem
      // type: update solutions in - neighborhood (1) or whole population (otherwise)
      int size;
      int time;
      
      time = 0;
      
      if (type == 1) {
        size = neighborhood_[id].Length;
      } else {
        size = population_.size ();
      }
      int[] perm = new int[size];
      
      RandomPermutation.execute (perm, size);
      
      for (int i = 0; i < size; i++) {
        int k;
        if (type == 1) {
          k = neighborhood_[id][perm[i]];
        } else {
          k = perm[i];
          // calculate the values of objective function regarding the current subproblem
        }
        double f1, f2;
        
        f1 = fitnessFunction (population_[k], lambda_[k]);
        f2 = fitnessFunction (indiv, lambda_[k]);
        
        if (f2 < f1) {
          population_.replace (k, new Solution (indiv));
          //population[k].indiv = indiv;
          time++;
        }
        // the maximal number of solutions updated is not allowed to exceed 'limit'
        if (time >= nr_) {
          return;
        }
      }
    }
    // updateProblem
    double fitnessFunction (Solution individual, double[] lambda)
    {
      double fitness;
      fitness = 0.0;
      
      if (String.Compare (functionType_, "_TCHE1") == 0) {
        double maxFun = -1.0e+30;
        
        for (int n = 0; n < problem_.numberOfObjectives_; n++) {
          double diff = Math.Abs (individual.objective_[n] - z_[n]);
          
          double feval;
          if (lambda[n] == 0) {
            feval = 0.0001 * diff;
          } else {
            feval = diff * lambda[n];
          }
          if (feval > maxFun) {
            maxFun = feval;
          }
        }
        // for
        fitness = maxFun;
      // if
      } else {
        //Console.WriteLine ("MOEAD.fitnessFunction: unknown type " + functionType_);
        //System.exit(-1);
      }
      return fitness;
    }
    // fitnessEvaluation
  }
}
