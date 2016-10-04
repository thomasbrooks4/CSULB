import java.sql.*;
import java.util.Scanner;

public class jdbcproject {
	private static String dbURL = "jdbc:mysql://cecs-db01.coe.csulb.edu:3306/cecs323h13";
	private static Connection connection = null;
	private static String userName = "cecs323h13";
	private static String password = "yeChoh";
	private static String teamsTable = "Software_Teams";
	private static String meetingsTable = "Meetings";
	private static String roomsTable = "Conference_Rooms";
	private static Statement stmt = null;
	private static Scanner inScan = new Scanner(System.in);

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		try {
			Class.forName("com.mysql.jdbc.Driver");
		} catch (ClassNotFoundException e) {
			System.out.println("Where is your MySQL JDBC Driver?");
			e.printStackTrace();
			return;
		}
		System.out.println("MySQL JDBC Driver Registered!");
		System.out.println();
		CreateConnection();

		if (connection != null) {
			doCRD();
		} else {
			System.out.println("Failed to make connection!");
		}
		if (connection != null) {
			try {
				connection.close();
			}
			catch (SQLException sqlExcept)
			{
				sqlExcept.printStackTrace();
			}
		}
		System.out.println("Thank you for using the program");
	}

	public static int getAChoice() {
		String choice=null;
		int intChoice = 0;
		System.out.println("Please select one of the following choices");
		System.out.println("1) List all team information");
		System.out.println("2) List information about a meeting");
		System.out.println("3) List all conference rooms and its status on a particular date");
		System.out.println("4) Reserve a meeting, to happen in the future (Room is for 1 team per day)");
		System.out.println("5) Insert a conference room");
		System.out.println("6) List meetings by purpose");
		System.out.println("0) Quit");
		System.out.println();
		do {
			choice = inScan.nextLine();
			System.out.println();
			try {
				intChoice = Integer.parseInt(choice);
				if (intChoice >= 0 && intChoice <= 6) break;
			} catch (NumberFormatException e) {
				System.out.println("Invalid input! please enter again:");
			}
		} while ( intChoice < 0 && intChoice > 6 );
		return intChoice;
	}
	/**
	 * doCRD: create, read, and delete
	 */
	public static void doCRD() {
		int choice;
		do {
			choice = getAChoice();
			switch (choice) {
			case 1:
				displayAllTeams();
				break;
			case 2:
				displayAllMeetings();
				break;
			case 3:
				displayAllRooms();
				break;
			case 4:
				reserveMeeting();
				break;
			case 5:
				insertRoom();
				break;
			case 6:
				listMeetingByPurpose();
				break;
			case 0: // exit
				return;
			}

		} while (true); // use a break statement to exit the loop
	}
	
	public static void CreateConnection() {
		try {
			// connect 
			connection = DriverManager
					.getConnection(dbURL,userName, password); 

		} catch (SQLException ex) {
			System.out.println("Connection Failed! Check output console");
			ex.printStackTrace();
		}
	}
	
	public static void displayAllTeams() {
		try {
			stmt = connection.createStatement();
			ResultSet results = stmt.executeQuery("select * from " + teamsTable + ";");
			ResultSetMetaData rsmd = results.getMetaData();
			int numberCols = rsmd.getColumnCount();
			
			for (int i=1; i<=numberCols; i++)
			{
				//print Column Names
				System.out.print(rsmd.getColumnLabel(i)+"\t");  
			}

			System.out.println("\n----------------------------------------------------------------------------");
			while(results.next())
			{
				String teamName = results.getString(1);               
				String teamLeader = results.getString(2);
				String teamDate = results.getString(3);
				String projectName = results.getString(4);

				System.out.format("%-18s\t%-15s %-16s\t%-55s\n", teamName, teamLeader, teamDate, projectName);

			}
			System.out.println("");
			results.close();
			rsmd=null;
			stmt.close();
		}
		catch (SQLException sqlExcept)
		{
			sqlExcept.printStackTrace();
		}
	}
	
	public static void displayAllMeetings() {
		try {
			stmt = connection.createStatement();
			ResultSet results = stmt.executeQuery("select * from " + meetingsTable + ";");
			ResultSetMetaData rsmd = results.getMetaData();
			int numberCols = rsmd.getColumnCount();
			
			for (int i=1; i<=numberCols; i++)
			{
				//print Column Names
				System.out.print(rsmd.getColumnLabel(i)+"\t"); 
				if (i == 3)
					System.out.print("\t");
			}

			System.out.println("\n----------------------------------------------------------------------------");
			while(results.next())
			{
				String teamName = results.getString(1);               
				String roomName = results.getString(2);
				String date = results.getString(3);
				String purpose = results.getString(4);

				System.out.format("%-18s\t%-15s %-10s\t%-15s\n", teamName, roomName, date, purpose);

			}
			System.out.println("");
			results.close();
			rsmd=null;
			stmt.close();
		}
		catch (SQLException sqlExcept)
		{
			sqlExcept.printStackTrace();
		}
	}
	
	public static void displayAllRooms() {
		try {
			stmt = connection.createStatement();
			ResultSet results = stmt.executeQuery("select * from " + roomsTable + ";");
			ResultSetMetaData rsmd = results.getMetaData();
			int numberCols = rsmd.getColumnCount();
			
			for (int i = 1; i <= numberCols; i++)
			{
				//print Column Names
				System.out.print(rsmd.getColumnLabel(i)+"\t");  
			}

			System.out.println("\n------------------------------------------------------------------------------");
			while(results.next())
			{
				String roomName = results.getString(1);               
				String roomNumber = results.getString(2);
				String buildingName = results.getString(3);
				String roomPhone = results.getString(4);
				String projectorType = results.getString(5);

				System.out.format("%-11s\t%-15s %-15s %-10s\t%-15s\n", roomName, roomNumber, buildingName, roomPhone, projectorType);

			}
			System.out.println("");
			results.close();
			rsmd=null;
			stmt.close();
		}
		catch (SQLException sqlExcept)
		{
			sqlExcept.printStackTrace();
		}
	}
	
	/**
	 * insertRecord: add a row to authors table
	 */
	public static void reserveMeeting() {
		try {
			String teamName, roomName, date, purpose;
			
			System.out.println("Which team is reserving the room?");
			teamName = inScan.nextLine();
			System.out.println("Which room?");
			roomName = inScan.nextLine();
			System.out.println("For what date? (Ex: 20141126 = 2014-11-26)");
			date = inScan.nextLine();
			System.out.println("For what purpose?");
			purpose = inScan.nextLine();
			
			stmt = connection.createStatement();
			stmt.executeUpdate("INSERT INTO Meetings (Software_Team_Name, Room_Name, Date, Purpose_of_Meeting) VALUES ('"
					+ teamName + "','" + roomName + "','" + date + "','" + purpose + "');");
			System.out.println("Insert completed!");
			System.out.println();
			stmt.close();
		}
		catch (SQLException sqlExcept)
		{
			sqlExcept.printStackTrace();
		}
	}
	
	public static void insertRoom() {
		try {
			String roomName, buildingName, projectorType, choice;
			int roomNumber, roomPhone;
			
			System.out.println("What is the room number?");
			choice = inScan.nextLine();
			roomNumber = Integer.parseInt(choice);
			System.out.println("What is the room name?");
			roomName = inScan.nextLine();
			System.out.println("What is the name of the building?");
			buildingName = inScan.nextLine();
			System.out.println("What is the room phone number?");
			choice = inScan.nextLine();
			roomPhone = Integer.parseInt(choice);
			System.out.println("What is the projector type?");
			projectorType = inScan.nextLine();
			
			stmt = connection.createStatement();
			stmt.executeUpdate("INSERT INTO Conference_Rooms (Room_Number, Room_Name, Building_Name, Room_Phone, Projector_Type)"
					+ " VALUES (" + roomNumber + ",'" + roomName + "','" + buildingName + "'," + roomPhone + ",'" + projectorType + "');");
			System.out.println("Insert completed!");
			System.out.println();
			stmt.close();
		}
		catch (SQLException sqlExcept)
		{
			sqlExcept.printStackTrace();
		}
	}
	
	public static void listMeetingByPurpose() {
		try {
			String purposeOrder = "", choice = "";
			int intChoice = 0;
			
			System.out.println("List all meetings for which purpose?");
			System.out.println("1) Planning");
			System.out.println("2) Demo");
			System.out.println("3) Team Working Session");
			System.out.println("4) Other");
			System.out.println();
			
			do {
				choice = inScan.nextLine();
				System.out.println();
				try {
					intChoice = Integer.parseInt(choice);
					if (intChoice >= 0 && intChoice <= 6) break;
				} catch (NumberFormatException e) {
					System.out.println("Invalid input! please enter again:");
				}
			} while (intChoice < 0 && intChoice > 6);
			
			switch (intChoice) {
			case 1:
				purposeOrder = "Planning";
				break;
			case 2:
				purposeOrder = "Demo";
				break;
			case 3:
				purposeOrder = "Team Working Session";
				break;
			case 4:
				purposeOrder = "Other";
				break;
			}
			
			stmt = connection.createStatement();
			ResultSet results = stmt.executeQuery("select * from " + meetingsTable + " where Purpose_of_Meeting = '" + purposeOrder + "';");
			ResultSetMetaData rsmd = results.getMetaData();
			int numberCols = rsmd.getColumnCount();
			
			for (int i=1; i<=numberCols; i++)
			{
				//print Column Names
				System.out.print(rsmd.getColumnLabel(i)+"\t"); 
				if (i == 3)
					System.out.print("\t");
			}

			System.out.println("\n----------------------------------------------------------------------------");
			while(results.next())
			{
				String teamName = results.getString(1);               
				String roomName = results.getString(2);
				String date = results.getString(3);
				String purpose = results.getString(4);

				System.out.format("%-18s\t%-15s %-10s\t%-15s\n", teamName, roomName, date, purpose);

			}
			System.out.println("");
			results.close();
			rsmd=null;
			stmt.close();
		}
		catch (SQLException sqlExcept)
		{
			sqlExcept.printStackTrace();
		}
	}
}
