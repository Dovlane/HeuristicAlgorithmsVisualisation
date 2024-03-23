using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeuristickiAlgoritmiProba;

namespace HeuristickiAlgoritmiProba
{
    interface IAlgorithm
    {
        void Iteration();
        bool AlgorithmFinished();
        string FinalReport();
    }
}