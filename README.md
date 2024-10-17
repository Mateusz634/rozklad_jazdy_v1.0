# BUS AND USER MANAGEMENT SYSTEM
## POLISH VERSION <a href="https://github.com/Mateusz634/rozklad_jazdy_v1.0/blob/master/README-PL.md">here</a>
### 1. Project Description
#### Application Name: Bus and User Management System

#### Description:
The application aims to facilitate the management of bus connections and users. It allows users to add, update, delete, and filter connections, as well as purchase tickets for selected trips. Additionally, the system manages user accounts, offering various levels of permissions.

#### Problem the application solves:
The application addresses the lack of a centralized system for managing bus schedules and users. It enables users to easily and quickly update their connections, purchase tickets, and manage their data, enhancing convenience and travel efficiency.

#### User Value:
Users gain convenient access to current schedules and the ability to purchase tickets, along with easy account management. Administrators have tools for effectively managing users and connections.

### 2. Application Architecture

| Module             | Description                                                                 |
| :---:              | :---                                                                      |
| Main Program       | The main entry point of the application that manages the menu.            |
| Connection List    | Stores all connections in list form.                                       |
| Schedule File      | Contains connection data in text format.                                   |
| Main Menu          | Allows selection of options for interaction with the application.          |
| AddConnection      | Adds a new connection to the list.                                        |
| UpdateConnection    | Updates an existing connection.                                          |
| DeleteConnection    | Deletes the selected connection.                                         |
| Filter              | Offers options for filtering connections.                                |
| BuyTicket           | Allows purchase of a ticket for a selected connection.                   |
| SaveSchedule        | Saves the current schedule state to a file.                              |
| Login/Management Module | Manages the login and registration process for users. (detailed in the flowchart below) |
<details>
  <summary>Show Flowchart</summary>
  <img src="https://github.com/user-attachments/assets/b6f53602-aebd-4837-8c82-f9dd8132e1c5" alt="Flowchart">
</details>

### 3. Function Descriptions:

| FUNCTION                | Functionality Description                                                                                                 |
| :---:                   | :---                                                                                                                    |
| `Main()`                | • The main function that starts the program and presents the menu for user option selection. <br> • Based on the user's choice, the function calls the appropriate methods. |
| `ShowCurrentSchedule()` | • Displays the current bus schedule. <br> • If the schedule list is empty, informs the user of the lack of connections. |
| `AddConnection()`       | • Allows the user to add a new connection in the format: CityA - CityB, Departure Time, Arrival Time, Ticket Price. <br> • Calculates travel time, considering arrival time after midnight. |
| `UpdateConnection()`    | • Enables updating an existing connection based on its index. <br> • Allows changing departure and arrival times while automatically updating travel time. |
| `DeleteConnection()`     | • Removes a connection from the list based on the index selected by the user. <br> • After deletion, displays a message that the connection has been removed. |
| `FilterConnections()`    | • The main filtering function allows the user to choose how to filter: by city, price, or travel time. |
| `FilterByCity()`         | • Filters connections based on the provided city.                                                                         |
| `FilterByPrice()`        | • Filters connections based on the maximum ticket price.                                                                  |
| `BuyTicket()`            | • Allows the user to purchase a ticket for a selected connection, saving ticket data to `tickets.txt`.                    |
| `SaveSchedule()`         | • Saves the current bus schedule to `schedule.txt` before exiting the program.                                            |
| `Login()`                | • Allows the user to log in to their account.                                                                              |
| `Register()`             | • Enables new users to create an account.                                                                                 |
| `EditUser()`            | • Allows the administrator to edit user data.                                                                             |
| `DeleteUser()`          | • Allows the administrator to delete a user account.                                                                      |

### 4. Installation and Running Instructions

#### 1. Download the source code of the application.

**Steps to download and run the application:**
1. **Download the application source code.**
   ```bash
   git clone https://github.com/Mateusz634/bus_management_system.git
# 2. Running the Application

## Steps to Run the Application:
1. **Open Visual Studio.**
2. **Load the project in Visual Studio.**
3. **Configure the paths for the files** `schedule.txt` **and** `tickets.txt`.
4. **Run the application by clicking** "Start".

## System Requirements
- **.NET Version**: .NET 5.0 or newer.
- **Operating System**: Windows 10 or newer.

### Follow the instructions displayed in the console to execute selected operations. 
![image](https://github.com/user-attachments/assets/c1893413-ba27-4916-8eaa-8d6a52876714)

### Operation:
First, a menu with options is displayed:
<br>
<img title="ActivityWatch" src="/1.png" align="center">

# 1. Login and Registration

## Steps to Log In:
1. **Log in**
2. **Register**

### Test
- **Login**: based on email and password.
- **Registration**: first name, last name, email address, password.

## After logging in, depending on permissions:

### **User:**
- Change user data
- Change password
- Delete account
- Log out

### **Administrator:**
- Manage users:
  a. Add user  
  b. Edit user  
  c. Delete user  
- View the list of users
- Log out

# 5. Code Documentation

Comments in the code describe key functions and logic, facilitating understanding of the program's operation.

## Description of the Most Important Code Segments:
- **Main**: Main application loop with user option handling.
- **AddConnection**: Function responsible for adding a new connection with time validation.
- **UpdateConnection**: Function that updates an existing connection, considering travel time logic.

# 6. Usage Examples

## Test Scenarios

### Adding a New Connection:
- **Input**: Warsaw - Krakow, 08:00, 10:00, 50 PLN
- **Expected Outcome**: Connection added.

### Updating a Connection:
- **Input**: Index 0, new times: 08:30, 10:30
- **Expected Outcome**: Connection updated.

### Filtering by Price:
- **Input**: up to 60 PLN
- **Expected Outcome**: Display connections up to 60 PLN.

## Examples of User Interaction:
The user selects an option from the menu, enters data, and receives appropriate feedback messages.

# 7. Error Handling

## Description of Error Handling:
The application handles errors related to incorrect data input (e.g., wrong time format, invalid connection index) through messages informing the user of the problem.

## List of Supported Exceptions:
- **FormatException**: When input data is not in the correct format (e.g., invalid times).
- **IndexOutOfRangeException**: When the user provides an invalid connection index.

### 8. Conclusions and Future Improvements

| Conclusions and Future Improvements                                                              |
| :---                                                                                             |
| Add a user login and registration module.                                                       | 
| Ability to edit passenger data when purchasing a ticket.                                        |
| The application operates as intended and allows effective management of connections.            |
| Encountered difficulties included issues with input data validation.                             |
| In the future, consider expanding features with additional options related to users.            |
| Implement additional security options such as two-factor authentication.                        |
| Expand functions with additional options related to users.                                       |
| Improve the user interface.                                                                       |

