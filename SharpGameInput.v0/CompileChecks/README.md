# Compile Checks

This checks to make sure that no compile errors will occur under stock .NET Framework when using any APIs of the library. These sanity checks need to be made since we force a language version on the main library's project, but we target .NET Standard 2.0 and use some features which shouldn't be publically exposed.
