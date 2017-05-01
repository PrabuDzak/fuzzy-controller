namespace FuzzyController
{
    static class SuhuKuah
    {
        public const string BIASA = "BIASA";
        public const string HANGAT = "HANGAT";
        public const string PANAS = "PANAS";

        public static int Index(string name)
        {
            switch (name)
            {
                case BIASA: return 0;
                case HANGAT: return 1;
                case PANAS: return 2;
                default: return -1;
            }
        }
    }

    static class SendokSambal
    {
        public const string TIDAK_PEDAS = "TIDAK_PEDAS";
        public const string SDKT_PEDAS = "SDKT_PEDAS";
        public const string PEDAS = "PEDAS";
        public const string SNGT_PEDAS = "SNGT_PEDAS";

        public static int Index(string name)
        {
            switch (name)
            {
                case TIDAK_PEDAS: return 0;
                case SDKT_PEDAS: return 1;
                case PEDAS: return 2;
                case SNGT_PEDAS: return 3;
                default: return -1;
            }
        }
    }

    static class EsTeh
    {
        public const string SEDIKIT = "SEDIKIT";
        public const string SEDANG = "SEDANG";
        public const string BANYAK = "BANYAK";
        public const string SNGT_BANYAK = "SNGT_BANYAK";

        public static int Index(string name)
        {
            switch (name)
            {
                case SEDIKIT: return 0;
                case SEDANG: return 1;
                case BANYAK: return 2;
                case SNGT_BANYAK: return 3;
                default: return -1;
            }
        }
    }
}
