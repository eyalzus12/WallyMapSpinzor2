# WallyMapSpinzor2
A C# library to deserialize, serialize, and render Brawlhalla's map XML files.

The library does not contain the implementation of the basic rendering (line drawing, rect drawing, image loading, etc), to allow for a custom implementation. Simply implement the ICanvas interface.