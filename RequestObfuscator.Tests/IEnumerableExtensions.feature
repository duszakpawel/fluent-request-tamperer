Feature: IEnumerableExtensions
	In order to execute an Action on every element of IEnumerable
	I want to be told how many times the action was invoked

@extensions
Scenario: Execute ForEach extension
	Given I have IEnumerable of 5 object
	When I invoke ForEach extension on this IEnumerable with any Action
	Then the Action should be invoked 5 times
