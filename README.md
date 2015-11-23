#MyFamilyConnect
A place to share news, photos, and happenings with your family.

BASIC ACCEPTANCE CRITERIA
=========================
* Users will be able to register and log in to the site.
* User will have a profile including basic biographical information.
* After login, user will be presented with the standard front page / dashboard displaying recent news and photos.
* User can create, read, update, and delete news items which the user created himself.
* User can create, read, update, and delete photo items which the user created himself.
* User can view and comment on posted items that are created by other users.
* Posted items can have an unlimited number of comments.

ADVANCED ACCEPTANCE CRITERIA
============================
* User can create, read, update, and delete calendar events which the user created himself.
* Users can communicate in real-time chat when they are logged in to the site.

#Basic Acceptance Criteria Entities

##UserProfile
* UserId: integer
* Owner: ApplicationUser
* FirstName: string
* LastName: string
* Birthdate: DateTime
* Address1: string
* Address2: string
* City: string
* State: string
* Zip: string
* PhoneNumber1: string
* PhoneNumber2: string
* Email: string
* AboutMe: string

##NewsPhotoItem: 
* NewsPhotoId: integer
* Title: string 
* Text: string
* HasPhoto: bool
* Photo: Image
* UserProfileId: integer
* Time_stamp: DateTime
* Comments: List<Comment> 

##Comment
* CommentId: integer
* Text: string
* UserProfileId: integer
* Time_stamp: DateTime 


#Advanced Acceptance Criteria Entities

##EventItem:
* EventId: integer
* Title: string
* Start_DateTime: DateTime
* End_DateTime: DateTime
* Description: string
* Created_by: ApplicationUser
* Time_stamp: DateTime
* Comments: List<Comment> 
