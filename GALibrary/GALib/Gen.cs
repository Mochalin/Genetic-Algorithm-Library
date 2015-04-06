using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
    ///<summary>  
    /// All gen representing classes must implement this interface
   /// GenTextForm - return the value of the gene in text form
   /// GenDigitalForm - returns gene in numerical form
   /// Change - change the value of the gene 
   ///</summary>
    public interface IGenProvide : ICloneable
    {
        string GetTextForm();
        double GetDigitalForm();
        void Change(string newValue);
    }
    //// Abstract class. Any genes class must inherit from it
    abstract public class Gen<T> : IGenProvide
    {
        public int id;
        protected short length;
        protected T genValue;
        virtual public T GenValue
        {
            get { return genValue; }
            set { genValue = value; }
        }
        virtual public short Length
        {
            get { return length; }
            set { length = value; }
        }
        protected Gen(short length, T genValue, int id = -1)
        {
            this.id = id;
            this.Length = length;
            this.GenValue = genValue;
        }
        abstract public string GetTextForm();
        abstract public double GetDigitalForm();
        abstract public void Change(string newValue);
        abstract public object Clone();

    }

    public class GenText : Gen<string>
    {
        public override string GenValue
        {
            get { return genValue; }
            set { genValue = value; }
        }
        public GenText(short length, string genValue, int id = -1) : base(length, genValue, id) { }
        public GenText(Gen<string> gen) : base(gen.Length, gen.GenValue, gen.id) { }
        public override object Clone()
        {
            return new GenText(Length, GenValue, id);
        }
        public override string GetTextForm()
        {
            return GenValue;
        }
        public override double GetDigitalForm()
        {
            return double.Parse(GenValue);
        }
        static public List<GenText> Create(short length, int count, List<string> values, int startId = 0)
        {
            List<GenText> gens = new List<GenText>(count);
            for (int i = 0; i < count; i++)
                gens.Add(new GenText(length, values[i], ++startId));
            return gens;
        }
        public override void Change(string newValue)
        {
            GenValue = newValue;
        }

    }
    /// <summary
    /// The class represents a gene. Binary coding is used 
    /// </summary>
    public class GenTextBinEncoding : GenText
    {
        static private Random rng = new Random();
        private double min;
        private double max;

        public double Min
        {
            get { return min; }
            set { min = value; }
        }
        public double Max
        {
            get { return max; }
            set { max = value; }
        }

        public GenTextBinEncoding(short length, string genValue, double min, double max, int id = -1)
            : base(length, genValue, id)
        {
            Min = min;
            Max = max;
        }
        public GenTextBinEncoding(GenTextBinEncoding gen) : this(gen.length, gen.GenValue, gen.Min, gen.Max, gen.id) { }

        public override double GetDigitalForm()
        {
            int value = Convert.ToInt32(GenValue, 2);
            return value * ((Max - Min) / (Math.Pow(2, Length) - 1)) + Min;
        }

        public override object Clone()
        {
            return new GenTextBinEncoding(Length, GenValue, Min, Max, id);
        }

        static public List<GenTextBinEncoding> Create(short length, int count, double min, double max, int startId = 0)
        {
            StringBuilder value = new StringBuilder();
            int maxvalue = Convert.ToInt16(new string('1', length), 2) + 1;
            List<GenTextBinEncoding> gens = new List<GenTextBinEncoding>(count);
            for (int i = 0; i < count; i++)
            {
                value.Clear();
                value.Append(Convert.ToString(rng.Next(0, maxvalue), 2).PadLeft(length, '0').ToString());
                gens.Add(new GenTextBinEncoding(length, value.ToString(), min, max, ++startId));
            }
            return gens;

        }
        static public List<List<GenTextBinEncoding>> Create(int numberOfDimensions, int count, List<short> length, List<double> min, List<double> max, int startId = 0)
        {
            List<List<GenTextBinEncoding>> gens = new List<List<GenTextBinEncoding>>(numberOfDimensions);
            for (int i = 0; i < numberOfDimensions; i++)
            {
                gens.Add(new List<GenTextBinEncoding>(count));
                gens[i] = Create(length[i], count, min[i], max[i], startId);
            }
            return gens;
        }


    }
    ///<summary>
    ///The class represents a gene. Gray coding is used 
    ///</summary>
    public class GenTextGrayEncoding : GenText
    {
        static private Random rng = new Random();
        private double min;
        private double max;

        public double Min
        {
            get { return min; }
            set { min = value; }
        }
        public double Max
        {
            get { return max; }
            set { max = value; }
        }

        public GenTextGrayEncoding(short length, string genValue, double min, double max, int id = -1)
            : base(length, genValue, id)
        {
            Min = min;
            Max = max;
        }
        public GenTextGrayEncoding(GenTextGrayEncoding gen) : this(gen.Length, gen.GenValue, gen.Min, gen.Max, gen.id) { }

        public override object Clone()
        {
            return new GenTextGrayEncoding(Length, GenValue, Min, Max, id);
        }
        public override double GetDigitalForm()
        {
            int value;
            StringBuilder bin = new StringBuilder();
            char temp;
            char previous = GenValue[0];
            bin.Append(previous);
            foreach (char c in GenValue.Substring(1, GenValue.Length - 1))
            {
                if (previous == '0')
                    temp = c;
                else
                {
                    temp = c == '0' ? '1' : '0';
                }
                previous = temp;
                bin.Append(temp);
            }
            value = Convert.ToInt32(bin.ToString(), 2);
            return value * ((Max - Min) / (Math.Pow(2, Length) - 1)) + Min;
        }


        static public List<GenTextGrayEncoding> Create(short length, int count, double min, double max, int startId = 0)
        {
            StringBuilder value = new StringBuilder();
            int maxvalue = Convert.ToInt16(new string('1', length), 2);
            List<GenTextGrayEncoding> gens = new List<GenTextGrayEncoding>(count);
            for (int i = 0; i < count; i++)
            {
                value.Clear();
                value.Append(Convert.ToString(rng.Next(0, maxvalue), 2).PadLeft(length, '0').ToString());
                gens.Add(new GenTextGrayEncoding(length, value.ToString(), min, max, ++startId));
            }
            return gens;
        }

        static public List<List<GenTextGrayEncoding>> Create(int numberOfDimensions, int count, List<short> length, List<double> min, List<double> max, int startId = 0)
        {
            List<List<GenTextGrayEncoding>> gens = new List<List<GenTextGrayEncoding>>(count);
            for (int i = 0; i < numberOfDimensions; i++)
            {
                gens.Add(new List<GenTextGrayEncoding>(count));
                gens[i] = Create(length[i], count, min[i], max[i], startId);
            }
            return gens;
        }
    }

   }
