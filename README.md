# Florida-Lottery-LM-Generator
Windows desktop app that generates lottery picks based on history and probability 

The Florida Lottery Desktop App

Created By Randy Harris 3/19/2016

This is a Windows Forms C# Desktop application.

This application was written in order to demonstrate basic C# skills. It is not for redistribution.

(IMPORTANT)
This application is dependent upon an MDF SQL database. The connection string must be configured in the app.config file in order for the app to function properly. 

The physical path to the database in the connection string located in the app.congig 
file must be modified to point to the path of the downloaded solution.
            
connectionString="DataSource=(LocalDB)\v11.0;AttachDbFilename=
C:\Users\randy.harris\Desktop\
FloridaLM\FloridaLM\FloridaLM\DatabaseFLLM.mdf;Integrated Security=True"

The application links to the Florida Lottery website and retrieves the drawing history for the Lucky Money game by parsing the HTML formatted data and storing it in the database. The application generates the top ten number sets (tickets to purchase) derived from an algorithm that utilizes the numbers most drawn in each position and when they are due to be drawn again. 
The algorithm is written in the form of a stored procedure (spGenNums). The definition of the algorithm is as follows:
Query the numbers in each draw position and order them by most occurring for the last 60 draws.
Each number is weighted by the longest period since the number was last drawn in each position.
The number possibilities are from 1 to 47 for positions 1, 2, 3, and 4. The last position is the Lucky Ball. Since the numbers 47, 46, and 45 can never occur in position one and numbers 1, 2, and 3 can never occur in position 4, the number of permutations are reduced which in fact reduces the total possibilities. Thus, there are 178,365 different ways in which 4 numbers can be chosen from a total of 47 unique numbers. The Lucky Ball may be between 1 and 17. The number of permutations increases substantively when including the Lucky Ball bringing the chance of actually guessing the correct combination to 1 in 3,032,205.

After applying the algorithm to each position, the top ten values in each position are used to generate a set of 10 best tickets to purchase.

The application uses the following C# .net libraries. These libraries and architecture were chosen so as to demonstrate C# skills in different areas and do not reflect the best practices in all cases. 

System;
System.Collections.Generic;
System.Linq;
System.Web;
System.Text;
System.Net;
System.IO;
System.Xml.Serialization;
System.Xml;
System.Data;
System.Net.NetworkInformation;
System.Threading.Tasks;
System.Windows.Forms;
System.ComponentModel;
System.Drawing;
