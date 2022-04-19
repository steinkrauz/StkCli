using System;

namespace StkCli
{
	class Options 
	{
		[AutoHelp]
		public bool Help {get; set;}
		[StrParam("-u","", true)]
		public string User {get; set;}
		private int _count;
		[IntParam("-c","--count")]
		public int Count {get => _count; 
			set {
				if (value<=0) throw new ArgumentException("Count cannot be negative or zero");
				_count = value;
			}
		}
		[FloatParam("-V","--volume")]
		public double Volume {get; set;}

		[FlagParam("-s","--sober")]
		public bool BeSober {get; set;}

		public Options(){
			Count = 1;
			Volume = 0.5;
		}
	}
    class Program
    {


	    static void Main(string[] args)
	    {
			ArgHandler<Options> ah = new();
			ah.Title = "StkCLi usage primer";
			ah.Copyright = "(c) Stein Krauz, 2022";
			Options o;

			try {
				o = ah.Parse(args);
			}catch(ArgumentException ex) {
				Console.WriteLine(ex.Message);
				return;
			}

			Console.WriteLine($"User is {o.User}");
			Console.WriteLine($"Count is {o.Count}");
			Console.WriteLine($"Volume is {o.Volume}");
			if (o.BeSober)
				Console.WriteLine("User must be sober");
	    }
    }
}
