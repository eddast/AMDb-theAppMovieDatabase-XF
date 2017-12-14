# Xamarin Forms Project for T-488-MAPP: the AMDb
## By Edda Steinunn Rúnarsdóttir

## Week 3: Xamarin Forms project - Visual Demonstration and Information
Note that images displaying app in this section are from tests conducted on an iPhone iOS 11 Simulator for iOS,
a Samsung Galaxy Alpha Mobile Phone (Android 5.0 with API 21) for Android and a Lenovo ThinkPad Computer for UWP.

### Application Structure and Purpose
The application has partially the same function as week one and two's iOS and Android app,
again a tabbed application was created but now with three tabs whose purpose is to fetch information about movies
from an external web service and displays that information appropriately.
This week's app however differs from the others because it is cross-platform compatible and fetches information
significantly faster than the other apps since it uses "lazy-load" (i.e. loads after rendering page/screen)
for some properties of movie information.

### First tab: Movie Search
The first tab is the tab user is automatically navigated to once app launches.
The first tab prompts user to write in a query substring of some movie into a search bar, then once user hits enter button on keyboard
a load spinner (generally very briefly) appears to indicate background process while resources are fetched from server. Once resources have been fetched,
A list appears in the view displaying results from user's query.
Some elements of the movie list is fetched asynchroniously like the poster and the movie cast displayed in each cell and therefore manifest later than the title and year.
The list of movies can be scrolled through.
Each item in list can be clicked and navigates user to a detail page displaying detail on the clicked movie.
Below are images that demonstrate the Search tab's functionality:

![alt text](https://image.ibb.co/cU9mZm/Search_Tab1.jpg)
![alt text](https://image.ibb.co/cdE9n6/Search_Tab2.jpg)

### Second tab: Top Rated Movies
This tab loads top rated movies from the external web service. The information is fetched when app launches (more specifically, when the first tab page has been rendered)
and initially displays a load spinner indicating background process, although in practise user will not see this as he will not be able to navigate to this tab quickly enough,
since the list is fetched quite quickly in general. Once resources have been retrieved, list is displayed. The list items are clickable like in the search view and once clicked navigate to another page showing movie details.
This list has a pull-to-refresh function, where when user pulls the list down, the list is fetched and rendered again in the view, thus updating any potential outdated data.
Below are images that demonstrate the Top Rated Movies tab's functionality:

![alt text](https://image.ibb.co/f8oDEm/Top_Rated_Tab.jpg)
(Note that in the first frame not all images and cast members have been fetched. This is intentional to demonstrate the "lazy-load")

### Third tab: Popular Movies
This tab has essentially the same functionality as the Top Rated Movies tab, except that movies displayed in this view's list
are movies that are popular at the moment instead of displaying all-time top rated movies. That is, list is fetched and can be pulled to refresh and list items are clickable and navigate to another page showing movie details.
Below are images that demonstrate the Popular Movies tab's functionality:

![alt text](https://image.ibb.co/eoZ3Em/Popular_Tab.jpg)

### The Movie Detail View
The detail view for some movie is accessible from every movie list present in the app. Every item in movie list in app is clickable
and navigates user to the movie detail view where basic information such as title and overview are instantly displayed while other information like images, runtime and tagline are fetched asynchroniously after this view has rendered.
Note that the detail view is very structually different from the structure we were supposed to implement by the project description.
This was because of attempting to achieve consistency in app appearance from the previous week's apps. Nontheless, the view displays all it is supposed to display.
The entire view can be scrolled (i.e. is inside a scrollview) and in addition to that, the movie overview can be scrolled too if it's size exceeds the height of the poster image beside it.
See images describing this functionality below:

![alt text](https://image.ibb.co/k9HZLR/Detail_View.jpg)
(In the second frame, the tagline and runtime have not yet been fetched to demonstrate the view's lazy load. Then after a brief period this is fetched and the end results is as shown in the third frame)

### Cross-Platform Compatibility
All function demonstrated above using iPhone simulator is preserved for both Android and UWP (except that UWP does not support the pull-to-load functionality)
Both are quite usable and have similar user interface. Below are visual demonstrations of these two remaining platforms although these images only to show the UI of the two platforms as functionality has already been demonstrated in details.

#### Android

The Android Search tab appearance (after searching for a query string):
![alt text](https://image.ibb.co/nCD5um/Android_Search.jpg)

The Android Top Rated tab appearance:
![alt text](https://image.ibb.co/eNngZm/Android_Top_Rated.jpg)

The Android Popular tab appearance:
![alt text](https://image.ibb.co/h2VDfR/Android_Popular.jpg)

#### UWP

The UWP Search tab appearance (after searching for a query string):
![alt text](https://image.ibb.co/bsD5um/UWPSearch.jpg)

The UWP Top Rated tab appearance:
![alt text](https://image.ibb.co/n6Oc76/UWPTop_Rated.jpg)

The UWP Popular tab appearance:
![alt text](https://image.ibb.co/jgWx76/UWPPopular.jpg)

#### Custom Rendering
Very minimal custom rendering is present in this project because that was considered unnecessary since all platforms display the information quite delightfully.
Although, the tab bar displayed on iOS is custom rendered and displays icons to enhance user experience because without it the apparence seemed unnatural.

### Enhancements from previous iOS and Android apps
Various changes were made in functionality, coding convention and code structure but one enhancement was above all those.
As previously stated, this week's Xamarin Forms application is significantly faster than the other apps created for the course as
some information is fetched asynchroniously ("lazy-loaded"), such as movie images, movie cast, movie tagline and movie runtime.
This does not greatly affect the user experience because even though page does not display full information initially,
it takes little time fetching the rest of the information.
To put this a bit into context, a minor experiment was conducted;
recieveing results with "Fargo" as a query string on last week's Android app took 12,29s,
while on the Xamarin Forms app it takes a mere 0.28s! (Both tests were conducted on Samsung Galaxy Alpha)
Moreover judging by this minor experiment, performance was enhanced by 98%!
(represented as percentage of speed increment for this specific experiment)

### Known limitations
The app uses significant memory due to many tasks running asynchroniously when resources are fetched which sometimes can cause problems on memory-sensitive devices.
For example, my phone is four years old (meaning ancient in phone-years) and sometimes crashes when a lot of information is fetched at once.
To make the application even run on my phone forced me to specify a large memory heap for the app's memory usage in the code.
I conducted tests however on other better and newer phones which had no problem running the app however the user navigated and used it.
In addition to this, the app can crash at random because of communication issues between either the app and the external web service
or between the web service and the movie database; for example if there is too much load on the server (this results in a somehow malformed response from server)
or if there is no internet connection. But since teacher stated that this would not be accounted for when grading these cases are not explicitly handled due to
limited time for the project.
