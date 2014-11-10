//
//  Distance.cs
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
//using jmetal.core;
//using jmetal.operators.comparator;

namespace Optimo_MOEAD
{
  static public class Distance
  {
    // The solution set to be ranked

/*
  public double [][] distanceMatrix(SolutionSet solutionSet) {
    Solution solutionI, solutionJ;

    //The matrix of distances
    double [][] distance = new double [solutionSet.size()][solutionSet.size()];        
    //-> Calculate the distances
    for (int i = 0; i < solutionSet.size(); i++){
      distance[i][i] = 0.0;
      solutionI = solutionSet.get(i);
      for (int j = i + 1; j < solutionSet.size(); j++){
        solutionJ = solutionSet.get(j);
        distance[i][j] = this.distanceBetweenObjectives(solutionI,solutionJ);                
        distance[j][i] = distance[i][j];            
      } // for
    } // for        
    
    //->Return the matrix of distances
    return distance;
  } // distanceMatrix
    
  public double distanceToSolutionSetInObjectiveSpace(Solution    solution,
                                      SolutionSet solutionSet) throws JMException{
    //At start point the distance is the max
    double distance = Double.MAX_VALUE;    
        
    // found the min distance respect to population
    for (int i = 0; i < solutionSet.size();i++){            
      double aux = this.distanceBetweenObjectives(solution,solutionSet.get(i));
      if (aux < distance)
        distance = aux;
    } // for
    
    //->Return the best distance
    return distance;
  } // distanceToSolutionSetinObjectiveSpace
    
   public double distanceToSolutionSetInSolutionSpace(Solution    solution,
                                      SolutionSet solutionSet) throws JMException{
     //At start point the distance is the max
     double distance = Double.MAX_VALUE;    
         
     // found the min distance respect to population
     for (int i = 0; i < solutionSet.size();i++){            
       double aux = this.distanceBetweenSolutions(solution,solutionSet.get(i));
       if (aux < distance)
         distance = aux;
     } // for
     
     //->Return the best distance
     return distance;
   } // distanceToSolutionSetInSolutionSpace
    

  public double distanceBetweenSolutions(Solution solutionI, Solution solutionJ) 
  throws JMException{                
    //->Obtain his decision variables
    Variable[] decisionVariableI = solutionI.getDecisionVariables();
    Variable[] decisionVariableJ = solutionJ.getDecisionVariables();    
    
    double diff;    //Auxiliar var
    double distance = 0.0;
    //-> Calculate the Euclidean distance
    for (int i = 0; i < decisionVariableI.length; i++){
      diff = decisionVariableI[i].getValue() -
             decisionVariableJ[i].getValue();
      distance += Math.pow(diff,2.0);
    } // for    
        
    //-> Return the euclidean distance
    return Math.sqrt(distance);
  } // distanceBetweenSolutions
    

  public double distanceBetweenObjectives(Solution solutionI, Solution solutionJ){                
    double diff;    //Auxiliar var
    double distance = 0.0;
    //-> Calculate the euclidean distance
    for (int nObj = 0; nObj < solutionI.numberOfObjectives();nObj++){
      diff = solutionI.getObjective(nObj) - solutionJ.getObjective(nObj);
      distance += Math.pow(diff,2.0);           
    } // for   
        
    //Return the euclidean distance
    return Math.sqrt(distance);
  } // distanceBetweenObjectives.
           
     */
  internal static void crowdingDistanceAssignment (SolutionSet solutionSet, int nObjs)
  {
    // <pex>
    if (solutionSet == (SolutionSet)null)
      throw new ArgumentNullException("solutionSet");
    if (solutionSet[0] == (Solution)null)
      throw new ArgumentNullException("solutionSet");
    // </pex>
    int size = solutionSet.size ();
    
    if (size == 0)
      return;
    
    if (size == 1) {
      solutionSet[0].crowdingDistance_ = Double.MaxValue ;
      //solutionSet.get(0).setCrowdingDistance(Double.POSITIVE_INFINITY);
      return;
    } // if
        
    if (size == 2) {
      solutionSet[0].crowdingDistance_ = Double.MaxValue;
      solutionSet[1].crowdingDistance_ = Double.MaxValue;
      return;
    } // if       
        
    //Use a new SolutionSet to evite alter original solutionSet
    SolutionSet front = new SolutionSet(size);
    for (int i = 0; i < size; i++){
      front.add(solutionSet[i]);
    }
        
    for (int i = 0; i < size; i++)
      front[i].crowdingDistance_ = 0.0 ;
        
    double objetiveMaxn;
    double objetiveMinn;
    double distance;
                
    for (int i = 0; i<nObjs; i++) {          
      // Sort the population by Obj n
      IComparer comp = new ObjectiveComparator(i) ;
      front.solutionList_.Sort(comp.Compare) ;
      //front.sort(new ObjectiveComparator(i));

        objetiveMinn = front[0].objective_[i] ;
      objetiveMaxn = front[front.size()-1].objective_[i] ;
      
      //Set de crowding distance            
      front[0].crowdingDistance_ = Double.MaxValue ;
      front[size -1].crowdingDistance_ = Double.MaxValue ;
      
      for (int j = 1; j < size-1; j++) {
        distance = front[j+1].objective_[i] - front[j-1].objective_[i];
        distance = distance / (objetiveMaxn - objetiveMinn);        
        distance += front[j].crowdingDistance_ ;
        front[j].crowdingDistance_ = distance;
      } // for
    } // for        
  } // crowdingDistanceAssing

    static public double distVector(double[] vector1, double[] vector2) {
      // <pex>
      if (vector1 == (double[])null)
        throw new ArgumentNullException("vector1");
      if (vector2 == (double[])null)
        throw new ArgumentNullException("vector2");
      // </pex>
    int dim = vector1.Length ;
    double sum = 0;
    for (int n = 0; n < dim; n++) {
      sum += (vector1[n] - vector2[n]) * (vector1[n] - vector2[n]);
    }
    return Math.Sqrt(sum);
  } // distVector



  } // Distance
}
