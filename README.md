# 1. C# assignment
Write console application that reads text file `data.txt` and counts number of each unique word in it. Words should be compared using current culture, case-**in**sensitive.

Results should be written to file `results.txt` (replaced if exists) where each line contains word in lower case followed by space and number of its occurrences in input file, sorted by that number and by word in alphabetical order (if number is the same).

For example if file contents is `abc, def, abc and egh` then results should be as follows:

> abc 2 <br> and 1 <br> def 1 <br> egh 1

It is safe to assume that number of unique words in the file does not exceed **10 000 000** and each word is no longer than **50** characters.
# 2. SQL assignment
You need to develop schema for database that will store logistic data for small machine parts shop. It operates with:
*	machine parts which have name;
*	regions which have name;
*	suppliers which have name;
*	deliveries which are made to certain region by certain supplier on certain date and consists of several lines; each line specifies machine part, its price in current delivery in **USD** dollars and number of these parts in this delivery.
	
Write down your schema and write SQL queries for obtaining info:

*	get list of suppliers’ names that made at least one delivery to region with name `A`
*	get summary value for all parts supplied by supplier `X` during **January 2000**
*	get number of deliveries with total cost more than **1000 USD** (total cost is sum of price times number of parts by all lines for this delivery), grouped by supplier (result table should have columns `Supplier name` and `Delivery number`)
*	get all supplier names who had not made any deliveries to regions where supplier `X` has any delivery
# 3. WPF assignment
Develop a small WPF application with single window which has text box, list box and OK button labeled `Number` and `Primes up to X:`, where **X** is the square of the number entered into text box (when value in the text box is updated – label and list box’s items should be updated immediately).

Text box’s background should be green if user entered valid positive integer and salmon otherwise.

OK button should be enabled if and only if user entered positive integer into text box and selected one item in the list box. By clicking **OK** – message box should be shown stating user’s choice.

You have to use **MVVM** pattern for this app.

Consider this example:
*	user enters `x` in the text box – text box’s background is **salmon**, list box is empty, **OK** button is disabled;
*	user enters `4` in the text box – text box’s background is **green**, list box contains items `2`, `3`, `5`, `7`, `1`” and `13`, nothing is selected, **OK** button is disabled; then user selects `5` – **OK** button becomes enabled; when clicked – message box `You have chosen 4 and 5` appears.
