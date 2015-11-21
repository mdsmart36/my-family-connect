# MyFamilyConnect
A place to share news, photos, and happenings with your family.

BASIC ACCEPTANCE CRITERIA
=========================
* First user will register as the site admin.
* Site admin will be able to invite family members and setup new user logins.
* User will be able to perform basic login administration (edit / save username, password, password hint).
* User will have a profile including basic biographical information and account settings.
* After login, user will have a "front page" displaying recent news, photos, and event agenda
* Users can post news, photos, and event items to the site.
* User can create, read, update, and delete news items which the user created himself.
* User can create, read, update, and delete photo items which the user created himself.
* User can create, read, update, and delete event items which the user created himself.
* User can view and comment on posted items that are visible to the user but created by another user.
* Posted items can have an unlimited number of comments.

Entities

##UserProfile
* FirstName
* LastName
* Birthday (DateTime)
* Address1
* Address2
* City
* State
* Zip
* PhoneNumber1
* PhoneNumber2
* Email
* AboutMe


##NewsItem: 
*	title (string), 
*	text (string), 
*	created_by (ApplicationUser), 
*	time_stamp (DateTime), 
*	comments (List<Comment>)

##PhotoItem:
*	title (string), 
*	photo (Image),
*	description (string), 
*	created_by (ApplicationUser), 
*	time_stamp (DateTime), 
*	comments (List<Comment>)

##EventItem:
*	Title (string),
*	start_DateTime (DateTime),
*	end_DateTime (DateTime),
*	description (string),
*	created_by (ApplicationUser),
*	time_stamp (DateTime),
*	comments (List<Comment>)

##Comment
*	text (string),
*	created_by (ApplicationUser),
*	time_stamp (DateTime)


##NewsList `List<NewsItem>`
##PhotoList `List<PhotoItem>`
##EventList `List<EventItem>`

ADVANCED ACCEPTANCE CRITERIA
============================

