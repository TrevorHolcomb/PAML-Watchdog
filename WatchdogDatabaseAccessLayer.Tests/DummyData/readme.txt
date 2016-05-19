This directory holds SQL files for quickly populating the DB during development.
This way we don't need to rely on using the AdminPortal.
Feel free to add your own.

If you're using the MessageGenerator, don't forget to use -r first.
Then edit the Rules section of dbo.Merged.sql to match the MessageTypeId.
Then run it.
Then generate your messages and run the watchdog.