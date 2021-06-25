# miBank
miBank

This contains the 3rd version of the MiBank applciation. 

There are 2 porjects within this solution one for the customer facing MiBank app built in c# and the admin angular applciation made for the admins of MiBank built with angular and c#.

The Admin application allows you to view, all the customers and change their details, view all the schdeuled payments on the system and block or unblock them as needed, view all the registered payees on the system and to view all transactions on the system and filter via: customer, account, account type, transaction type, date range and account range. These results are anaylised with 3 different graphs, one for the number of transactions per customer, the total dollor amount for each type of transaction and how many of each type of transaction has been made.

The customer MiBank application, expands on the oringal by adding extra secuitry measures and background services to make the exprince better for the users.

Notes:

There are some current issues with the filter for the transaction page and if you select conflicting fields can result in a blank form and no results. You will need to undo the filters to reset the form

To Start: 

To start the application you can either in visual studio start both projects at the same time or otherwise run up the MiBank app first then in your comand line spin up the admin application. 

Each project can be started indpendtly and tested however to user the admin login from Mibank to the admin app, both projects will need to be running

Accessing Admin Portal:

From the client website while not logged in click the 'Admin' link in the top right.
Username: Admin
Password: Admin

Once login is successful client site will redirect you to Admin portal.

To run this application you will need to fill in your on server details (This was orignally built using an azure server)
