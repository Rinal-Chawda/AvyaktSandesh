# AvyaktSandesh

Backend apis:

ArticlesController
•	GET    /api/Articles
Get all articles (with media files).
•	GET    /api/Articles/filter?language={language}&title={title}&year={year}
Filter articles by language, partial title, and year.
Returns: article fields with titleId and the title string (no media files).
•	POST   /api/Articles
Create a new article.

TitlesController
•	GET    /api/Titles
Get all titles (with articles).
•	GET    /api/Titles/{id}
Get a specific title by ID (with articles).
•	POST   /api/Titles
Create a new title.
•	PUT    /api/Titles/{id}
Update a title.
•	DELETE /api/Titles/{id}
Delete a title.


MediaController
•	POST   /api/Media/upload
Upload a media file for an article.
•	DELETE /api/Media/{id}
Delete a media file by ID.
•	GET    /api/Media/by-article/{articleId}
Get all media files for a specific article.
