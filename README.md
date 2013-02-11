PropertyChangeLogger is a library you can use to easily detect changes made to properties on an object.

Have your business class inherit from PropertyChangeLoggingClass.
The initial values are automatically stored when you call the empty constructor of your class, or can be set manually by calling the SetInitialPropertyValues() method.
You can see if any properties were changed by checking the HasChangedProperties value.
You will get a list of all changed properties by checking the ChangedProperties value.  This returns a list of the property names, original values, and current values.
If your property is another business object that has PropertyChangeLoggingClass as its base, changes to its properties will be shown in the parent class.  This will search recursively, detecting changes to grand-child, great-grand-child, etc. properties.
This does not currently handle properties that are lists.  

If you have questions or suggestions, please contact me at github@scottlilly.com

Thanks,
Scott Lilly
