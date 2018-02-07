namespace FTAnalyzer
{
    public class StandardisedName
    {
        public bool IsMale { get; private set; } 
        public string Name { get; private set; }
        
        public StandardisedName(int sex, string name)
        {
            IsMale = sex != 1;  // 1 female, 2 male, anything else male
            Name = name;
        }

        public StandardisedName(bool male, string name)
        {
            IsMale = male;
            Name = name;
        }

        public override string ToString()
        {
            return (IsMale ? "Male :" : "Female :") + Name;
        }
    }
}
