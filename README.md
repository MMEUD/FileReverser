Reverser
========

Basic Code Exercise: Text File Reverser Console Application

Please provide in a text file a section of self crafted code (in a language of your choice) with associated unit tests that demonstrates a test driven approach to implement the feature provided (see features folder):

Feature: Reverse Text File
	In order to read the contents of a text file that contains text that is back-to-front
    As a reader
    I want to create a file that has reversed the content of the text file

Scenario: Reverse input text
	Given I enter the file name {input file}
	And the file contains {input text}
	When I ehter the file name {output file} and press return
	Then the file {output file} is created
	And the contents of the file contains {output text}

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
