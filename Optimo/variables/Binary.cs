using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using jmetal.core;
//using jmetal.util;
using System.Diagnostics;

namespace Optimo
{
  internal class Binary : Variable
  {
    public BitArray bits_
    {
      get;
      set;
    }

    public int numberOfBits_
    {
      get;
      set;
    }

    public Binary()
    {
    } //Binary


    public Binary(int numberOfBits)
    {
      numberOfBits_ = numberOfBits;

      bits_ = new BitArray(numberOfBits_);
      for (int i = 0; i < numberOfBits_; i++)
      { 
        if (PseudoRandom.Instance().NextDouble() < 0.5)
        {
          bits_[i]  = true;
        }
        else
        {
          bits_[i] = false;
        }
      }
    } //Binary

    public Binary(Binary variable)
    {
      numberOfBits_ = variable.numberOfBits_;

      bits_ = new BitArray(numberOfBits_);
      for (int i = 0; i < numberOfBits_; i++)
      {
        bits_[i] =  variable.bits_[i];
      }
    } //Binary

    public override object Clone()
    {
      return new Binary(this);
    }
  }
}
