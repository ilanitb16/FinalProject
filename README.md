# T-Rex Game Project

## Introduction

This project is a customized version of the classic "T-Rex Game" by Google. It includes additional features such as colorful graphics, extra characters, backgrounds, upgrades, and a scoring system. The game is designed to be simple to understand and enjoyable for all users, even those with minimal experience with computer games.

## Technologies Used

- Universal Windows Platform (UWP)
- Languages: SQL, C#, XAML

## Game Overview

The game starts immediately upon pressing the "Play" button, with the character automatically navigating a path. Obstacles in the form of signs and walls are scattered along the path, which the player must jump over to avoid elimination. Jumping over obstacles earns points, with additional points awarded for jumping over walls and signs. The points accumulated contribute to a leaderboard displayed within the game. Collecting coins scattered on the field earns virtual money, allowing players to purchase additional characters and backgrounds.

## How to Run the Game

1. Clone this repository `git clone https://github.com/ilanitb16/FinalProject.git`
2. Open the project in Visual Studio.
3. Run the game.
   
## Game Interface
The game interface includes various pages such as the home page, help page, virtual store, login page, settings, leaderboard page, and more. Each page serves a specific function within the game, facilitating gameplay, customization, and interaction with other players. The screens and instructions are included here.

### Game Page
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/8bdfdb56-32d7-4d9b-8b93-ae1a5a73e690)
![WhatsApp Image 2024-05-19 at 11 24 48_8cfe6fa3](https://github.com/ilanitb16/FinalProject/assets/97344492/e360761c-acdb-4da5-a98d-993bc6cc61e6)
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/2afb047a-c6ad-4363-8e1d-21c5a0086e39)

### Available characters 
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/38c9f7c6-9604-4893-a822-b95d447280cf)
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/de2d48d7-fc47-49d7-b13d-47144c0983c7)
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/ca3da3c6-5544-4cd5-a487-2c07a566ec9f)


### Opening page
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/172a62ae-be63-4c62-b644-57a6fc58fb4e)
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/04b183ae-d8b5-40b5-a206-0176ccaec6ab)

### Login Screen
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/3a0eb247-ea73-4a1a-91b0-cf1da88344c6)
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/5f1e9fe5-ca41-4d3a-b525-8259388a58a4)

In the login screen, when entering a username/password that does not match the values ​​in the database, the system alerts you of an error when entering a wrong username or incorrect password.
Click the "OK" button for the option to return to the fields, correct them and try to connect again.

### Forgot Password Window
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/1937fc0a-6ea4-4947-9cee-4765b4e744e9)

When clicking on the "Forgot password?" button, you can recover your password by entering your username and email address. If we click "ok" and the user exists, the user's password will be displayed. If not, a corresponding message will be displayed:
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/6eceb7c5-4d05-481d-a4df-ed6aaac93306)

### Registration Screen
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/13fd7cb1-30e6-4b33-b9e3-c28ff2528074)

- If the entered user already exists, a corresponding message will appear alerting the existence of a user with identical data. The user will not be able to register and the system will not add him to the database. Clicking the "OK" button allows you to return to the fields, correct them and try to connect again.

![image](https://github.com/ilanitb16/FinalProject/assets/97344492/b621e568-397c-4a07-8ea8-80502604a4a6)

- If one field remains empty, the system will alert you with an appropriate message. The user will not register.
Clicking the "OK" button allows you to return to the fields, correct them and try to connect again.

![image](https://github.com/ilanitb16/FinalProject/assets/97344492/06ab7dd0-f42f-4b1a-803c-a6623f9787c7)

- If the passwords are not the same, you will receive a message on the screen informing you of this. The user will not register.
Clicking the "OK" button allows you to return to the fields, correct them and try to connect again.

![image](https://github.com/ilanitb16/FinalProject/assets/97344492/cd27b134-2114-4e2b-9041-26c5c2aeb534)

### Score Screen
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/5bef8e52-2d64-45fc-9c46-ddc869b00f6b)

Three players with the most points are shown on the podium. The first place is represented by a gold medal, the second place by a silver medal and the third place by a bronze medal.
The score of the connected character (the connected user) appears at the top of the screen on the right side. In addition to the score, the amount of coins the player has accumulated is written.

### Prizes and Obstcles overview
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/a9112f4f-5c4f-40f9-81b2-449beb996f70)

### Game Over Screen
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/bdae9470-9a1a-4cac-ac8b-d3c0819ab301)

When the game ends, a notification pops up about it, and the character will stop running. At the same time, there is a specific sound for loss. Clicking on the "OK" button closes the notification, and allows you to click on the "BACK" button that will lead to a return to the main page.

### Logout Screen
![image](https://github.com/ilanitb16/FinalProject/assets/97344492/92ce7ca9-ca93-43bd-8088-def0f3a16480)
When the user is logged in, on the main page the icon that represented the "login" button changes to log out. Clicking on it disconnects the user. (the icon on the bottom right)

![image](https://github.com/ilanitb16/FinalProject/assets/97344492/f5e334d3-640b-4364-945d-53e4c905f12f)

## Conclusion

This project offers an entertaining gaming experience with simple mechanics and customizable options. It aims to be accessible to users of all levels of experience with computer games.
