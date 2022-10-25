password = "this is Password"

query = "1 = 1"

import mysql.connector

mydb = mysql.connector.connect(
  host="localhost",
  user="yourusername",
  password="yourpassword",
  database="mydatabase"
)

mycursor = mydb.cursor()

mycursor.execute("SELECT * FROM customers" + query)

myresult = mycursor.fetchall()

for x in myresult:
  print(x)

print(password)