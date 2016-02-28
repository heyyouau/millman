Useage:
Please provide the location of the total temp file and the configuration file (in that order) as arguments separated by spaces.
An optional third argument can be provide to specify the name of the output file. (Otherwise, the default filename "output.txt" will be used)


Assumptions:
1. Different calculations can take place on the same Variable Type, however these may use different period sets 
	(e.g. MinValue for one calculation instead of FirstValue for another)
2. The scenario value is not included in the example output, so I have assumed for the sake of the excercise that
	it is not to be taken into account when aggregating the results (i.e. no grouping of results based on scenario, only VariableType and calculation)
3. The test output shows the resultant average as 134444848.1, however, when calculated out, this actually calculates to 134444848.06. I have not rounded
	any of my output.
4. The specification mentioned that the files had a .txt suffix.  It wasn't clear how important that is, and I have based my validity on the 
	format of the files and the data they contained rather than the names of the files or their suffixes.
5. The specification doesn't mention that the files are auto located, so I have assumed that they need to be passed in as parameters when invoking the app.


Further Dev?
1. Needs more unit tests around error conditions? Not specified what should take place on bad data - I suspect whole process should fail quickly and loudly 
	as errors on a per line basis would impact all aggregate calculations? That's  the approach I've taken anyway. Also no spec on how logging / errors should
	be reported - I've just written to console for the purposes of the app.


	 

