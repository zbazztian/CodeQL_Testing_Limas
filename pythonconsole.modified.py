import mysql.connector
from flask import Flask, request

app = Flask(__name__)

@app.route('/endpoint')
def endpoint():

  password = "this is Password"

  query = "1 = 1"                                  # Not under attacker-control, because it is hard-coded.
  tainted_query = request.args.get('query', '')    # Under attacker-control, originating from outside of the application.


  mydb = mysql.connector.connect(
    host="localhost",
    user="yourusername",
    password="yourpassword",
    database="mydatabase"
  )

  mycursor = mydb.cursor()

  mycursor.execute("SELECT * FROM customers" + query)          # Does not trigger an alert because "query" is not under attacker-control.

  myresult = mycursor.fetchall()

  for x in myresult:
    print(x)

  mycursor.execute("SELECT * FROM customers" + tainted_query)  # Triggers an alert because "tainted_query" is under attacker-control.

  myresult = mycursor.fetchall()

  for x in myresult:
    print(x)

  print(password)
