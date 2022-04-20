# StkCli

It's a trivial command line parser library. For any complex, production grade project it's better to use [commandline library](https://github.com/commandlineparser/commandline). But sometimes, especially for a small pet project, it just feels wrong to pull in such a vast library to have a couple of simple switches. So, here it is, a simple drop-in source file for hassle-free command line parsing.

## Features

  * Short, long or both forms for an argument
  * String, integer and float values
  * Flags
  * Auto help generator
 
 ## Quick start
 
 Using this library is as simple as one-two-three.
 
 1. Add ``StkCli.cs`` to your project.
 2. Create a class to hold your options with StkCli attributes. The following attributes are supported:
   *  StrParam -- just a string parameter
   *  IntParam -- a signed 32-bit integer
   *  FloatParam --  a floating point value. Beware, system locale is used to parse
   *  FlagParam -- sets boolean property to true. No value required.
   *  AutoHelp -- shows basic help from params' names and descriptions and exits the program
 
 Each attribute definition has four attibutes: short name, long name, mandatory flag and description. Flag and description are optional. You may use short or long form, or both at the same time. The unused form is defined with an empty string.
 
 <details>
  <summary>See example</summary>
  
 ```cs
   class Options
  {
      //put AutoHelp before any mandatory param or missing value error will be triggered
      [AutoHelp]
      public bool Help {get; set;} // just a dummy prop

      //A mandatory string param with a short name only
      [StrParam("-u","", Mandatory=true)]
      public string User {get; set;}

      //need a storage field beause of non-trivial setter
      private int _count;
      //Attribute must be on the property, not field
      [IntParam("-c","--count")]
      public int Count {get => _count;
      // here we can add some value checks. Throw an ArugmentException to handle it within library
      set {
              if (value<=0) throw new ArgumentException("Count cannot be negative or zero");
              _count = value;
          }
      }

      [FloatParam("-V","--volume", false, "Volume of one bottle")]
      public double Volume {get; set;}

      [FlagParam("-s","--sober")]
      public bool BeSober {get; set;}

      //the constructor is a good place to put the defaults.
      public Options(){
            Count = 1;
            Volume = 0.5;
      }
   }
```
</details>
  
  3. Call `ArgHandler` class to fill it with values from the command line. This class has two constructors. The default one would create a new instance of a given configuration storage class. The second constructor will take an existing instance and work with it. So you can, for example, deserialize it from a stored form and then override some parameters with command line values.
  
  <details>
    <summary>See example</summary>
    
   ```cs
    //call the parser
    ArgHandler<Options> ah = new();
    //ArgHandler has two properties to show with AutoHelp
    ah.Title = "StkCLi usage primer";
    ah.Copyright = "(c) Stein Krauz, 2022";
    Options o;
    try {
        o = ah.Parse(args);
    }catch(ArgumentException ex) {
        Console.WriteLine(ex.Message);
        return;
    }
   ```
