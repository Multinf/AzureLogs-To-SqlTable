##Azure HTTP Web Server Error Logs to SQL

First of all, you will need enable the error logs in your application:

*Settings > Diagnostic logs > Web Server logging > Detailed error messages*
![image](https://raw.githubusercontent.com/Multinf/AzureLogs-To-SqlTable/master/docs/img.PNG)

And that's it!

You could even transform this console app to a webjob easily.

###Setup
1. Clone the repo
2. Create the necessary tables and stored procedure [Click here](https://github.com/Multinf/AzureLogs-To-SqlTable/blob/master/scripts/create_table_and_stored_procedure.sql)
3. Set the values in [App.Settings](https://github.com/Multinf/AzureLogs-To-SqlTable/blob/master/src/App.config)
4. Run!

###Issues
- Do you found an issue? Post right here!
