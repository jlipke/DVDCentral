BEGIN
	INSERT INTO [dbo].[tblMovie] (Id, Title, Description, ImagePath, Cost, InStockQty, RatingId, FormatId, DirectorId)
	VALUES
	(1, 'Spider-Man: Far From Home','Peter Parkers relaxing European vacation takes an unexpected turn when Nick Fury 
	shows up in his hotel room to recruit him for a mission. The world is in danger as four massive elemental creatures
	each representing Earth, air, water and fire emerge from a hole torn in the universe. Parker soon finds himself donning
	the Spider-Man suit to help Fury and fellow superhero Mysterio stop the evil entities from wreaking havoc across the continent', 'spiderman.jpg', 10.00, 15, 3, 1, 1),

	(2, 'Toy Story 4', 'Woody, Buzz Lightyear and the rest of the gang embark on a road trip with Bonnie and a new toy named Forky.
	The adventurous journey turns into an unexpected reunion as Woodys slight detour leads him to his long-lost friend Bo Peep. As Woody
	and Bo discuss the old days, they soon start to realize that theyre worlds apart when it comes to what they want from life as a toy.', 'toystory4.jpg', 12.00, 23, 1, 1, 2),

	(3, 'Beetle Juice', 'After Barbara and Adam Maitland die in a car accident, they find themselves stuck haunting their country residence,
	unable to leave the house. When the unbearable Deetzes and teen daughter Lydia buy the home, the Maitlands attempt to scare them away 
	without success. Their efforts attract Beetlejuice, a rambunctious spirit whose "help" quickly becomes dangerous for the Maitlands and innocent Lydia.', 'beetlejuice.jpg', 4.00, 2, 2, 2, 3)
	

END