# text file reverser based on basic code exercise

Feature: Reverse Text File

  	# using a scenario outline of simple characters

	Scenario Outline: Output reversed text to a file

		Given I have a command line window open
		And I type in a <inputfile> of saved text file that contains the <inputtext>
		And I type in a <outputfile>
		When press return
		Then the command line reads “the input file: <inputfile> has been reversed 
		And output to the file: <output filename> with the reversed content: <outputtext>"
		And the file <output filename> has been created
		And the contents of the file contains the reverse of the input as: <output text>

	Example
		| inputfile	| outputfile	|
    		| a		| a		|
    		| abcd		| dcba	 	|
    		| abcdef12345	| 54321fedcba	|  
