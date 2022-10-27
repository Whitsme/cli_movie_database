// Aaron Whitaker
// Winter 2022
// CIS 207
// Console Assignment 5: JSON Movies Collection Program

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ConsoleAssignment5
{
    // Class 'Program' calls class 'MovieCollection' as an object, sets and imports the JSON folder into object 'MovieCollection', requests user input for what method to call, calls said method via object 'MovieCollection', and finally calls method 'SaveAllJSON' (while passing var 'jsonFolder') to save all edits to JSON files // required inputs: JSON folder, class 'MovieCollection', and user input for method selection // expected outputs: var'jsonFolder' to method 'InpurtJSON', user selected method, and finally method 'SaveAllJSON' with var 'jsonFolder' passed to said method // no returned values  
    internal class Program
    {
        static void Main(string[] args)
        {
            MovieCollection myMovies = new MovieCollection() { };
            string jsonFolder = "JSON";
            myMovies.ImportJSON(jsonFolder);
            Console.WriteLine("\nPlease choose from the below (you may also hit enter to exit):\n");
            Console.WriteLine("1: Show me the movies currently available");
            Console.WriteLine("2: I want to add a movie");
            Console.WriteLine("3: I want to add or update the movie ratings");
            Console.WriteLine("4: Exit program\n");
            var menuChoice = Console.ReadLine();
            if (menuChoice != "")
            {
                try { Convert.ToInt32(menuChoice); }
                catch (Exception main) { Console.WriteLine(main.Message); }
            }
            else { menuChoice = "4"; }
            try
            {
                if (Convert.ToString(menuChoice) == "1") { myMovies.ShowAllMovies(); }
                else if (Convert.ToString(menuChoice) == "2") { myMovies.AddMovie(); }  
                else if (Convert.ToString(menuChoice) == "3") { myMovies.UpdateRating(); }
                else if (Convert.ToString(menuChoice) == "4") { Environment.Exit(0); }
            }
            catch (Exception main) { Console.WriteLine("Experienced an error in main method." + main.Message); }
            myMovies.SaveAllJSON(jsonFolder);
        }
    }
    // Class 'MovieCollection' creates list object using class 'Movies', method 'InportJSON' is called from class 'Program', user selected method below (not 'InportJSON' or 'SaveAllJSON') is called from class 'Program', method 'SaveAllJSON' is called from class 'Program'  // required inputs: JSON folder , class 'Movies', and user input for method selection along with any input required by chosen method // expected outputs: edits to JSON movie files are saved either back to modified file or new file if adding a movie // no returned values other than adding items to list object using class 'Movies'  
    class MovieCollection
    {
        List<Movies> theMovies = new List<Movies> { };
        public void ImportJSON(string jsonDirectory)
        {
            string[] jsonFiles = Directory.GetFiles(jsonDirectory); 
            Movies oneMovie = new Movies(); 
            string jsonData = ""; 
            foreach (var jsonFile in jsonFiles)
            {
                jsonData = File.ReadAllText(jsonFile); 
                oneMovie = JsonSerializer.Deserialize<Movies>(jsonData);
                theMovies.Add(oneMovie); 
            }
        }
        public void ShowAllMovies()
        {
            Console.WriteLine();
            Console.WriteLine("Movies currently in the JSON folder:\n");
            try {
                foreach (var aMovie in theMovies) { if (aMovie != null) { Console.WriteLine(aMovie.title + " " + aMovie.year + " " + aMovie.rating); } }
            }
            catch (Exception readMovies) { Console.WriteLine("\nExperienced error trying to read the movies in JSON folder. Please check path and JSON file format.\n" + readMovies.Message);}
            Console.WriteLine("\nPlease reopen program to make another selection.");
        }
        public void AddMovie()
        {
            Movies AdditionalMovie = new Movies();
            Console.WriteLine("\nHit enter only to return to exit program.\n");
            Random id = new Random();
            try { AdditionalMovie.id = ($"tt{id.Next(0, 1000000).ToString("D7")}"); }
            catch (Exception idGenerator) { Console.WriteLine("\nEncountered and error when auto generating a movie ID.\n" + idGenerator.Message); }
            bool validTitle = false;
            Console.WriteLine("Please enter the movie title:");
            try
            {
                var addTitle = Console.ReadLine();
                if (addTitle == "") { Environment.Exit(0); }
                else { AdditionalMovie.title = addTitle; }
                validTitle = true;
            }
            catch (Exception input) { Console.WriteLine("\nPlease enter letters or numbers only."); Console.WriteLine(input.Message); }
            while (!validTitle) ;
            bool validYear = false;
            Console.WriteLine("Please enter the movie year:");
            try
            {
                var addYear = Console.ReadLine();
                if (addYear == "") { Environment.Exit(0); } 
                else if (Convert.ToInt32(addYear) <= 2022 & Convert.ToInt32(addYear) >= 1895) { AdditionalMovie.year = addYear; }
                else { Console.WriteLine("\nInvalid year input. Please enter a valid year ~ >= 1895 next time\n"); }
                validYear = true;
            }
            catch (Exception input)
            { Console.WriteLine("\nInvalid year input. Please enter a valid year ~ >= 1895 next time\n" + input.Message); }
            while (!validYear) ;
            bool validRuntime = false;
            Console.WriteLine("Please enter the movie runtime:");
            try
            {
                var addRuntime = Console.ReadLine();
                if (addRuntime == "") { Environment.Exit(0); } 
                else if (Convert.ToInt32(addRuntime) <= 999 & (Convert.ToInt32(addRuntime) >= 1)) { AdditionalMovie.runtime = addRuntime; }
                else { Console.WriteLine("\nInvalid runtime input. Please enter a valid runtime in minuets next time"); }
                validRuntime = true;
            }
            catch (Exception input)
            { Console.WriteLine("\nInvalid runtime input. Please enter a valid runtime in minuets next time.\n" + input.Message);}
            while (!validRuntime) ;
            bool validGenre = false;
            Console.WriteLine("Please enter the movie genre:");
            try
            {
                var addGenre = Console.ReadLine();
                if (addGenre == "") { Environment.Exit(0); }
                else { AdditionalMovie.genre = addGenre; }
                validGenre = true;
            }
            catch (Exception input)
            { Console.WriteLine("\nPlease enter letters or numbers only."); Console.WriteLine(input.Message); }
            while (!validGenre) ;
            bool validRating = false;
            Console.WriteLine("Please enter the movie rating (*-*****):");
            try
            {
                var addRating = Console.ReadLine();
                if (addRating == "") { Environment.Exit(0); }
                else if (addRating == "*" || addRating == "**" || addRating == "***" || addRating == "****" || addRating == "*****") { AdditionalMovie.rating = addRating; }
                else { Console.WriteLine("\nPlease enter a valid rating, either *-*****.\n"); }
                validRating = true;
            }
            catch (Exception input)
            { Console.WriteLine("\nPlease enter letters or numbers only."); Console.WriteLine(input.Message); }
            while (!validRating) ;
            theMovies.Add(AdditionalMovie);
            Console.WriteLine("\nPlease reopen program to make another selection.");
        }
        public void UpdateRating()
        {
            foreach (var aMovie in theMovies)
            {
                try
                {
                    if (aMovie.title != null) 
                    { 
                        Console.WriteLine(aMovie.title + " " + aMovie.year + " Rating: " + aMovie.rating); 
                        string movieRating = Console.ReadLine();
                        if (movieRating == "*" || movieRating == "**" || movieRating == "***" || movieRating == "****" || movieRating == "*****") 
                        {
                            aMovie.rating = movieRating;
                        }
                        else if (movieRating == null || movieRating == string.Empty) { break; }
                        else { Console.WriteLine("\nPlease enter a valid rating, either *-*****.\n"); }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred when we tried to add the rating.");
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("\nPlease reopen program to make another selection.");
        }
        public void SaveAllJSON(string jsonFolder)
        {
            foreach (var aMovie in theMovies)
            {
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonData = JsonSerializer.Serialize(aMovie, jsonOptions);
                StreamWriter writer = new StreamWriter(jsonFolder + "/" + aMovie.id + ".json");
                writer.Write(jsonData);
                writer.Close();
            }
        }
    }
    // class 'Movies' is used to organize, edit, and save movies and modifications to movie data to and from the JSON files in JSON folder // No inputs are required, but it accepts input from class 'MovieCollection' // no expected outputs // no returned values
    class Movies
    {
        public string id { get; set; }
        public string title { get; set; }
        public string year { get; set; }
        public string runtime { get; set; }
        public string genre { get; set; }
        public string rating { get; set; }
    }
}
