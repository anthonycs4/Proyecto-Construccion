import pyodbc

server = "LAPTOP-LN12JSH"  # Asegúrate de que es el nombre correcto
database = "Proyecto"
driver = "ODBC Driver 17 for SQL Server"

try:
    conn_str = f"DRIVER={driver};SERVER={server};DATABASE={database};Trusted_Connection=yes"
    conn = pyodbc.connect(conn_str)
    print("✅ Conexión exitosa a SQL Server")
except Exception as e:
    print(f"❌ Error de conexión: {e}")
