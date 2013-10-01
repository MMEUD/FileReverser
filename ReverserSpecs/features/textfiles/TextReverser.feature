@changing
Feature: Reverse Text File
	In order to read the contents of a text file that contains text that is back-to-front
    As a reader
    I want to create a file that has reversed the content of the text file

Scenario: Reverse input text
	Given I enter the file name "texttoreverse.txt"
	And the file contains "abcdef12345"
	When I enter the file name "textreversed.txt" and press return
	Then the file "textreversed.txt" is created
	And the contents of the file contains "54321fedcba"



	
