using System;
using System.Collections.Generic;

namespace FuzzyController
{
    abstract class FuzzyFunction
    {
        public double sLeft, eRight;
        public string name;

        public virtual double centerPoint
        {
            get { return (sLeft + eRight) / 2; }
        }

        public bool isInInterval(double t)
        {
            return t > sLeft && t < eRight;
        }

        public abstract double GetMembershipValue(double t);
    }

    class FuzzyTrapezoid : FuzzyFunction
    {
        public double eLeft, sRight;

        public FuzzyTrapezoid(string name, double sLeft, double eLeft, double sRight, double eRight)
        {
            this.name = name;
            this.sLeft = sLeft;
            this.eLeft = eLeft;
            this.sRight = sRight;
            this.eRight = eRight;
        }

        public override double GetMembershipValue(double t)
        {
            if (t < sLeft)
            {
                return 0;
            }
            else if (t < eLeft)
            {
                return (t - sLeft) / (eLeft - sLeft);
            }
            else if (t <= sRight)
            {
                return 1;
            }
            else if (t < eRight)
            {
                return (eRight - t) / (eRight - sRight);
            }
            else
            {
                return 0;
            }
        }
    }

    class FuzzyTriangle : FuzzyFunction
    {
        public double middle;

        public override double centerPoint
        {
            get
            {
                if (middle != sLeft && middle != eRight)
                {
                    return middle;
                }
                else
                {
                    if (middle == sLeft) return sLeft;
                    else return eRight;
                }
            }
        }

        public FuzzyTriangle(string name, double sLeft, double middle, double eRight)
        {
            this.name = name;
            this.sLeft = sLeft;
            this.middle = middle;
            this.eRight = eRight;
        }

        public override double GetMembershipValue(double t)
        {
            if (t < sLeft)
            {
                return 0;
            }
            else if (t <= middle)
            {
                return (t - sLeft) / (middle - sLeft);
            }
            else if (t < eRight)
            {
                return (eRight - t) / (eRight - middle);
            }
            else
            {
                return 0;
            }
        }
    }

    class FuzzySet
    {
        public FuzzyFunction[] function;

        public double minLimit { protected set; get; }
        public double maxLimit { protected set; get; }

        public FuzzySet(double minLimit, double maxLimit, FuzzyFunction[] _function)
        {
            function = _function;
            this.minLimit = minLimit;
            this.maxLimit = maxLimit;
        }

        public FuzzyFunction GetFunctionByName(string name)
        {
            for (int i = 0; i < function.Length; i++)
            {
                if (function[i].name == name)
                {
                    return function[i];
                }
            }
            return null;
        }

        public FuzzyFunction[] GetMembership(double t)
        {
            List<FuzzyFunction> listF = new List<FuzzyFunction>();
            for (int i = 0; i < function.Length; i++)
            {
                if (function[i].isInInterval(t))
                {
                    listF.Add(function[i]);
                }
            }

            return listF.ToArray();
        }
    }
}
