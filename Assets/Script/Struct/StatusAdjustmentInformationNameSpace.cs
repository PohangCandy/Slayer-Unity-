namespace StatusAdjustmentInformationNameSpace
{
    public struct SAInformation
    {
        public SAInformation(string _target, int _turn, string _category, int _amount)
        {
            target = _target;
            turn = _turn;
            category = _category;
            amount = _amount;
        }
        public string target;
        public int turn;
        public string category;
        public int amount;
    }
}
