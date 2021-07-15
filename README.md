# DealerOn-CodingInterview - Alec Trievel

## Description

This is my submission for the DealerOn coding interview. The solution corresponds to the second problem - Sales Tax

## Design process

There are four main aspects in the application that I considered:
* Configuration
* Accepting input / reading the receipt
* Calculating taxes and totals
* Display the final output

Each aspect is separated into unique classes to assure they only have one responsibility and are loosely-coupled from each other. 

There are also three projects in the solution containing the code:
* Common - the business logic 
* Runner - run the console application
* Tests - unit tests 

### Configuration

I used the built-in configuration manager (Microsoft.Extensions.Configuration) to allow the sales tax rate, import rate, and round-to values to be easily modified in case the values would change in the future. This prevents unnecessary code changes when doing calculations since they are not hard-coded values.

### Accepting input

Since the prompt said input can be accepted in any way, I chose to allow the user to enter a path to a JSON file. JSON is a widely used format and there are tons of libraries that make parsing the file into objects trivial. By using an interface (IDataReader in Common.IO), however, this could easily be swapped for a CSV, XML, plain text, etc. solution as long as the code follows the contract the interface describes. 

Inside Common.Models, there are two classes - LineItem and Receipt. The JSON file is parsed to a list of LineItems, and the Receipt class is used to hold the values for the final output.

### Calculating taxes and totals

The Calculator class in Common.Utils is reponsible for doing all the calculations described by the requirements. The class accepts constructor arguments for the tax rate, import duty rate, and the position we want to round to so it can be as flexible as possible. 

There is also an interface - ICalculator - which allows for new classes to be easily swapped in for this implementation (i.e. maybe there is a new law that changes how taxes ae calculated).

### Displaying the final output

As with the other classes, there is an interface (IReceiptWriter) that must be implmented by anything wanting to display the end result. I chose to display them as console output for simplicity, but this could just as easily be written to a file, database, or some other output.

## Running the application

This is a .NET 5 console application that should be able to run just about anywhere .NET 5 is installed. The main project is called Runner, which runs the console app. 

I personally ran and developed the app using Visual Studio for Mac without issue.

## Testing the application

The unit test project - Tests - contain a series of unit tests that check the bulk of the underlying logic in the application. All tests are passing as of the latest push.

## Assumptions

* I assumed there must be a flag that indicates if an item is taxable or not and does not rely on the item's name/description to determine taxes (i.e. a book of matches is not a book, but it contains "book." The book is not taxed, whereas the matches the should be)
* I also assumed there must be a flag to indicate if an item has import duties or not. This is for similar reasons as metioned above for the taxes. 

## Test files

I provided some sample JSON files that match the samples in the PDF. They are located in the /sample-files directory