using system;


namespace LibRarisma
{
    
   public static class Math
   {
       private static new Random RandInt; //Doesnt expose RandInt
       
       
       ///Allows user to use on instance of random.next
       ///This should reduce duplicates when calling new Random
       public static int32 Random(int32 Minimum, int32 Maximum) {return Randint.Next(Minimum,Maximum);}   
   
       //Turns any number into a positive
       //Eg input -123 and get 123.
       public static int32 MakePostive(int32 input) { if (input < 0) {return input * -1;} else {return input;} }
       
    
       //Just add a, b and c!
       public static List<int32> Quadratic_Formula(int a, int b, int c)
       {
           int positivex = b * -1 + sqrt(b*b - 4*a*c);
           int negativex = b * -1 - sqrt(b*b - 4*a*c);
           List<int32> Output = new List<int32>();
           
           Output.add(postivex / 2 * a);
           Output.add(negativex / 2 * a);
           
           return Output;
       }
   
   
       public static int64 mean(List<int32> ListOfNumbers)
       { return ListOfNumbers.sum() / ListOfNumbers.Count();}
       
       public static int64 range(List<int32> ListOfNumbers)
       { return ListOfNumbers.Maximum() - ListOfNumbers.Minimum();}
   
   }
   
}
