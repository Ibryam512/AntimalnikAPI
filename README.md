# AntimalnikAPI

## What is Antimalnik

Antimalnik is a web app in which students and the staff of SU "Ekzarh Antim I" can sell and buy school stuff (books, uniforms, etc.), can publish and find lost things on the territory of the school and can communicate.

## What is AntimalnikAPI?

AntimalnikAPI is the back-end part of the web application. It contains different end-points that help for the communication between the database and the front-end of the prokect.

| Function | Path | Type | What does it do? |
|----------|------|------|------------------|
| Get All Posts | /posts | GET | Gets an information about all published posts. |
| Get Certain Post | /posts/{id} | GET | Gets an information about a certain post. |
| Add Post | /posts | POST | Publish post in the database. |
| Edit Post | /posts | PUT | Edits a certain post. |
| Delete Post | /posts/{id} | DELETE | Deletes post with a certain primary key. |
| Delete Post | /posts/{id} | DELETE | Deletes post with a certain primary key. |
| Get Sent Messages | /messages/{userName}/sent | GET | Gets the sent messages by a user with the given username. |
| Get Recieved Messages | /messages/{userName}/recieved | GET | Gets the recieved messages by a user with the given username. |
