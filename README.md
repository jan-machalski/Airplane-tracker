# Airplane Tracker

This project contains a program for the simulation and tracking of aviation-related objects, including airplanes, airports, cargo, and crew members.
One of the goals of the project was to use various design patterns such as **Factory**, **Adapter**, **Decorator**, **Command**, etc.

---

## Flight Tracking GUI

The planes are placed in correct locations by interpolating coordinates and start/landing times.

![Flight Tracking GUI](https://github.com/user-attachments/assets/cebc1ce7-d424-4a81-a03c-a6f29b6d2a31)

---

## Commands

Data in the database can be edited using commands:

### Display Data

Displays objects from the database that meet the specified conditions.

```bash
# Example: Display all from Airport where ASML >= 1000
display * from Airport where ASML >= 1000
```

**Output:**

![Database Output](https://github.com/user-attachments/assets/3f61fd7c-3374-4d37-90da-7f8fc52232ac)

---

### Add Data

Adds a new object to the database.

```bash
add PassengerPlane new ID=7777 Serial=1234 Model=Airbus FirstClassSize=20 EconomyClassSize=100
```

---

### Update Data

Updates an existing object in the database.

```bash
update PassengerPlane set FirstClassSize=25 where ID=7777
```

---

### Delete Data

Deletes an object from the database.

```bash
delete PassengerPlane where ID=7777
