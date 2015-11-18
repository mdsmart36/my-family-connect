# my-family-connect
A place to share news, photos, and happenings with your family.

BASIC ACCEPTANCE CRITERIA
=========================
* First user will register as the site admin.
* Site admin will be able to invite family members and setup new user logins.
* User will be able to perform basic login administration (edit / save username, password, password hint.
* User will have a profile including basic biographical information and account settings.
* After login, user will have a "front page" displaying recent news, photos, and event agenda
* Users can post news, photos, and event items to the site.
* User can create, read, update, and delete news items that he posts to the site.
* User can create, read, update, and delete photo items that he posts to the site.
* User can create, read, update, and delete event items that he posts to the site.
* User can view and comment on posted items that are visible to the user but created by another user.
* Posted items can have an unlimited number of comments.

Entities

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


##NewsList
##PhotoList
##EventList

ADVANCED ACCEPTANCE CRITERIA
============================

