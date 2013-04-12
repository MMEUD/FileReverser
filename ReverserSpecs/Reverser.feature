Feature: Reverse Text File
	In order to more easily read the contents of a text file in a mirror
    As a reader
    I want to create a file that has reversed the content of a text file

Scenario: Reverse input text
	Given I enter an "c:\github\texttoreverse.txt"
	And the file contains the "abcdef12345"
	When I ehter an "c:\github\textreversed.txt" and press return
	Then the "c:\github\textreversed.txt" is created
	And the contents of the file contains the reverse of the input as "54321fedcba"
