create database AirlineAppDB

use AirlineAppDB

create table BookFlight
(
	flightNo int primary key,
	source varchar(20),
	destination varchar(20),
	fare int,
	totalSeats int
)

create table BookTicket
(
	passengerId int primary key,
	FlightNo int,
	passengerFistName varchar(30),
	passengerLastName varchar(30),
	City varchar(30),
	Age int,
	Foreign key (FlightNo) references BookFlight(FlightNo)
)

insert into BookFlight (flightNo, source, destination, fare, totalSeats) values (100, 'ADL', 'MEL', 139, 180)
insert into BookFlight (flightNo, source, destination, fare, totalSeats) values (101, 'MEL', 'ADL', 119, 180)
insert into BookFlight (flightNo, source, destination, fare, totalSeats) values (102, 'ADL', 'SYD', 189, 180)
insert into BookFlight (flightNo, source, destination, fare, totalSeats) values (103, 'PER', 'MEL', 239, 180)

