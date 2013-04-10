Reverser
========

Basic Code Exercise: Text File Reverser Console Application

Please provide in a text file a section of self crafted code (in a language of your choice) with associated unit tests that demonstrates a test driven approach to implement the feature provided (see features folder):

Feature: Reverse Text File

	Scenario Outline: Output reversed text to a file

		- Given I have a command line window open
		- And I type in a <inputfile> of saved text file that contains the <inputtext>
		- And I type in a <outputfile>
		- When press return
		- Then the command line reads “the input file: <inputfile> has been reversed 
		  and output to the file: <output filename> with the reversed content: <outputtext>"
		- And the file <output filename> has been created
		- And the contents of the file contains the reverse of the input as: <output text>

	Example
		| inputfile		| outputfile	|
    	| a				| a		 		|
    	| abcd			| dcba	 		|
    	| abcdef12345	| 54321fedcba	|    	

Please note the following constraints:

Demonstrate a TDD approach.
Unit tests should not hit the file system although integration tests can be categorised and included

The code needs to adhere to the following criteria:
- Followed instructions
- Use of Test Driven Development
- Followed YAGNI
- Overall Readability

Score out of 5 for each criteria totalling a maximum score of 20
