# Funcular.Ontology
##### A coherent and convenient set of interfaces and base classes for entity domains. 

* Dispense with boilerplate persistence code. 
* Apply Id assignment functions to the entire domain.
* Centralize handling of timestamp and audit field logic. 
* Add extension methods to like-sets of entities. 

### Examples 
```csharp
// Derive entities from one of the base classes in Archetypes.
// Funcular.Ontology.Archetypes.Createable<TId> is a simple abstract class 
// with an Id of <TId> (string in this case), a DateCreatedUtc datetime, 
// and a CreatedBy of <TId> (again, string here). Every other abstract
// class in Archetypes derives from this:
	public partial class TransactionItem : Createable<string>
	{
		public Decimal ItemAmount { get; set; }
		public String ItemText { get; set; }
	}
	
	// Statically assign an Id-generating function to the base type.
	// Here is a horrible example of an Id function:
	Createable<string>.IdentityFunction = () => Datetime.Now.ToString();
	// If you're using the Base36 Id generator with dependency injection:
	Createable<string>.IdentityFunction = () => _base36.NewId();
```

### Type Dependency Diagram
This diagram shows the inheritance tree of the Funcular.Ontology.Archetypes namespace:

![Dependency Diagram](/Content/DependencyGraph.png?raw=true "Dependency Graph")

### Leveraging by Funcular.DataProviders

If you use Funcular.DataProviders with Ontology-derived entities, you get some boilerplate behavior taken care of for free.
* Id assignment is done entirely in Ontology
* DataProviders takes care of assigning Create dates, users
* Also, assigns Modify dates and users

```csharp
// Named, Labeled and Described are abstract classes in Archetypes, with
// (as you might expect), Name, Label and Description properties. These
// are cumulative; Labeled derives from Named, and Described from Labeled.
// These in turn derive from Modifyable, which adds DateModifiedUtc [datetime?]
// and ModifiedBy [<TId>] to Createable. 
	public partial class ThingWithADescription : Described<string>
	{
	    /* The following properties are inherited from the Archetypes hierarchy: 
	       string Id { get; set; } // Id generation logic set in static method
	       datetime DateCreatedUtc { get; set; }
	       string CreatedBy { get; set; }
	       DateTime? DateModifiedUtc { get; set; }
	       string ModifiedBy { get; set; }
	       string Name { get; set; }
	       string Label { get; set; }
	       string Description { get; set; }
	     */
	     
      // You just add properties specific to this class, e.g.:	     
      public int? HowManyCatsDoesSheHave { get; set; }
      public bool IsItTuesday { get; set; }
      public string Nickname { get; set; }
	}
	
      // Tell the provider about the current user's identity; 
      // this will eventually be replaced with a UserProvider
      // instance supplied by dependency injection:
      _dataProvider.SetCurrentUser<string>(currentUserId);
      
      // This will assign CreateDateUtc and CreatedBy:
      _dataProvider.Insert<ThingWithADescription, string>(myThing);
      
      // This will assign ModifyDateUtc and ModifiedBy:
      _dataProvider.Update<ThingWithADescription, string>(myThing);
      
```

### Class Diagram
This class diagram shows the signature of each interface and abstract class in the Funcular.Ontology.Archetypes namespace:

![Class Diagram](/Content/ClassDiagram.png?raw=true "Class Diagram")