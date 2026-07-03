# Bill of Materials Generator

Creates structured Bills of Materials (BOM) for manufacturing widgets by aggregating components like circles, rectangles, and ellipses with their dimensions and properties.

## Key Components
- **Models**: `IWidget`, `Circle`, `Rectangle`, `Eclipse` (interchangeable shape models)
- **Interfaces**: `IBillOfMaterialsGenerator`, `IWidgetValidator`, `ILogger`
- **Services**: BOM generation services that process widget collections

## Functionality
- Accepts a list of widgets (circles, rectangles, ellipses)
- Validates each widget before processing
- Generates a formatted BOM string listing all components
- Supports extensible widget types through IWidget interface

## Usage
1. Create widget instances (Circle, Rectangle, etc.)
2. Validate widgets using IWidgetValidator
3. Pass validated list to IBillOfMaterialsGenerator
4. Receive formatted BOM string output