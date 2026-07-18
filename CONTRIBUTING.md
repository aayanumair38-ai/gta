# Contributing to GTA 3D

## Code Style

### C# Conventions
- Use PascalCase for class names and public methods
- Use camelCase for private fields and local variables
- Use meaningful variable names
- Add XML documentation for public methods

### Example
```csharp
public class MyClass : Node3D
{
    /// <summary>Does something important</summary>
    public void DoSomething()
    {
        int localVariable = 0;
        // Implementation
    }
}
```

## Adding New Features

1. Create a new script in appropriate folder
2. Follow the existing architecture patterns
3. Add comments explaining complex logic
4. Test thoroughly before committing
5. Update documentation

## Bug Reports

When reporting bugs, include:
- Clear description of the issue
- Steps to reproduce
- Expected vs actual behavior
- Godot version and OS

## Pull Requests

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add/update tests
5. Update documentation
6. Submit PR with clear description

## Performance Guidelines

- Avoid creating objects every frame
- Use object pooling for frequently spawned items
- Cache frequently accessed nodes
- Profile with Godot's built-in profiler
- Minimize draw calls

## Code Review Checklist

- [ ] Code follows project conventions
- [ ] No unused imports or variables
- [ ] Proper error handling
- [ ] Documentation updated
- [ ] No performance regressions
- [ ] Tests passing
