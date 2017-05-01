using System;
using System.Collections.Generic;

namespace FuzzyController
{
    class Program
    {
        static string RuleBase(string kuah, string sambal)
        {
            int s = SendokSambal.Index(sambal);
            int k = SuhuKuah.Index(kuah);

            string[,] outputBase = new string[,] 
            {
                {EsTeh.SEDIKIT, EsTeh.SEDIKIT, EsTeh.SEDANG },
                {EsTeh.SEDIKIT, EsTeh.SEDANG, EsTeh.SEDANG },
                {EsTeh.SEDANG, EsTeh.BANYAK, EsTeh.BANYAK },
                {EsTeh.BANYAK, EsTeh.SNGT_BANYAK, EsTeh.SNGT_BANYAK }
            };

            return outputBase[s, k];
        }

        static void Main(string[] args)
        {
            double suhuKuah = 40;               // Control input
            double sendokSambal = 11.7;         // Control input
            double seruputEsTeh;                // Control Output

            FuzzySet setSuhuKuah;               // Fuzzy Input
            FuzzySet setJumlahSendokSambal;     // Fuzzy Input
            FuzzySet setEsTeh;                  // Fuzzy Output

            setSuhuKuah = new FuzzySet(0, 100, new FuzzyFunction[]
            {
                new FuzzyTrapezoid(SuhuKuah.BIASA,  0,  0,  35,  45),
                new FuzzyTriangle(SuhuKuah.HANGAT, 35, 45,  55),
                new FuzzyTrapezoid(SuhuKuah.PANAS, 45, 55, 100, 100)
            });

            setJumlahSendokSambal = new FuzzySet(0, 15, new FuzzyFunction[] 
            {
                new FuzzyTrapezoid(SendokSambal.TIDAK_PEDAS, 0,   0,  2,  4),
                new FuzzyTrapezoid(SendokSambal.SDKT_PEDAS,  2,   4,  6,  8),
                new FuzzyTrapezoid(SendokSambal.PEDAS,       6,   8, 10, 12),
                new FuzzyTrapezoid(SendokSambal.SNGT_PEDAS, 10, 12, 15, 15)
            });

            setEsTeh = new FuzzySet(0, 450, new FuzzyFunction[]
            {
                new FuzzyTriangle(EsTeh.SEDIKIT,       0,   0, 150),
                new FuzzyTriangle(EsTeh.SEDANG,        0, 150, 300),
                new FuzzyTriangle(EsTeh.BANYAK,      150, 300, 450),
                new FuzzyTriangle(EsTeh.SNGT_BANYAK, 300, 450, 450)
            });

            // Fuzzy-fy

            Console.WriteLine("Suhu Kuah: " + suhuKuah);
            Console.WriteLine("Sendok Sambel: " + sendokSambal);
            Console.WriteLine();

            FuzzyFunction[] fuzzyMembershipKuah = setSuhuKuah.GetMembership(suhuKuah);
            FuzzyFunction[] fuzzyMembershipSambal = setJumlahSendokSambal.GetMembership(sendokSambal);

            for (int i = 0; i < fuzzyMembershipKuah.Length; i++)
            {
                Console.WriteLine("Membership Kuah: " + fuzzyMembershipKuah[i].name + " " + fuzzyMembershipKuah[i].GetMembershipValue(suhuKuah));
            }

            for (int i = 0; i < fuzzyMembershipSambal.Length; i++)
            {
                Console.WriteLine("Membership Pedes: " + fuzzyMembershipSambal[i].name + " " + fuzzyMembershipSambal[i].GetMembershipValue(sendokSambal));
            }

            Console.WriteLine();

            // Defuzzy-fy

            // Strength Of Rule

            Dictionary<string, double> strengthOfRule = new Dictionary<string, double>();
            for (int i = 0; i < fuzzyMembershipKuah.Length; i++)
            {
                for (int j = 0; j < fuzzyMembershipSambal.Length; j++)
                {
                    string candidateEsTeh = RuleBase(fuzzyMembershipKuah[i].name, fuzzyMembershipSambal[j].name);
                    double ruleValue = Math.Max(fuzzyMembershipKuah[i].GetMembershipValue(suhuKuah), fuzzyMembershipSambal[j].GetMembershipValue(sendokSambal));

                    if (!strengthOfRule.ContainsKey(candidateEsTeh))
                    {
                        strengthOfRule.Add(candidateEsTeh, ruleValue);
                    }
                    else
                    {
                        strengthOfRule[candidateEsTeh] = Math.Max(strengthOfRule[candidateEsTeh], ruleValue);
                    }

                    Console.WriteLine("Strength of Rule: " + candidateEsTeh + " " + ruleValue);
                }
            }
            Console.WriteLine();

            // Center Weight Method

            double defuzzyValue = 0;
            double defuzzyDivide = 0;
            foreach (KeyValuePair<string,double> rule in strengthOfRule)
            {
                defuzzyValue += setEsTeh.GetFunctionByName(rule.Key).centerPoint * rule.Value;
                defuzzyDivide += rule.Value;

                Console.WriteLine(string.Format("fungsi es teh {0}    \tcenter: {1}  rule value: {2}", rule.Key, setEsTeh.GetFunctionByName(rule.Key).centerPoint, rule.Value));
            }
            Console.WriteLine();

            seruputEsTeh = defuzzyValue / defuzzyDivide;

            Console.WriteLine("Seruput Es Teh: " + seruputEsTeh + " ml");
            Console.ReadKey();
        }
    }
}
