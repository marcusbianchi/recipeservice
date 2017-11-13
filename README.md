# PhaseAPI
API to Manage Recipes on Lorien. Used to create, update, read and delete Recipes. Also responsible for managing its products and Parameters
## Phase Product Data Format
These are the fields of the phase product and it's constrains:
- phaseProductId: Id of the Product inside de Phase given by de Database.
  - Integer
  - Ignored on Create, mandatory on the other methods
- productId: Original ID of the Product.
  - Integer
  - Mandatory
- value: Quantity of the given product in the phase.
  - String (Up to 50 chars)
  - Mandatory
- measurementUnit:Unit of measurement of the given product
  - String (Up to 50 chars)
  - Mandatory
- product: Product  as saved on the Product API
  - Product Object (Composed by: productId,producName,producCode,productGTIN,childrenProductsIds)
  - Ignored on Create, mandatory on the other methods  
### JSON Example:
```json
{
    "phaseProductId": 3,
    "productId": 2,
    "value": "50",
    "measurementUnit": "kg",
    "product": 
    {
      "productId": 2,
      "producName": "Nome Teste 2",
      "producCode": "TesteCode",
      "productGTIN": "+9999999",
      "childrenProductsIds": []
    }
}
```
## Phase Parameter Data Format
These are the fields of the phase parameter and it's constrains:
- phaseParameterId: Id of the Parameter inside de Phase given by de Database.
  - Integer
  - Ignored on Create, mandatory on the other methods
- parameterId: Original ID of the Parameter.
  - Integer
  - Mandatory
- setupValue: Target value of the parameter for the given Phase.
  - String (Up to 50 chars)
  - Mandatory
- minValue: Minimum acceptable value for the Parameter.
  - String (Up to 50 chars)
  - Mandatory
- maxValue: Minimum acceptable value for the Parameter.
  - String (Up to 50 chars)
  - Optional
- measurementUnit:Unit of measurement of the given product
  - String (Up to 50 chars)
  - Mandatory
- parameter: Parameter as saved on the Parameter API
  - Parameter Object (Composed by: productId, producName, producCode, productGTIN, childrenProductsIds, ThingGroup, groupName, groupCode)
  - Ignored on Create, mandatory on the other methods  
### JSON Example:
```json
{
  "phaseParameterId": 2,
  "parameterId": 1,
  "setupValue": "50",
  "measurementUnit": "kg",
  "minValue": "12",
  "maxValue": "13",
  "parameter": 
  {
    "parameterId": 1,
    "parameterName": "da",
    "parameterCode": "teste",
    "thingGroupId": 1,
    "thingGroup": 
    {    
      "thingGroupId": 1,
      "groupName": "teste",
      "groupCode": "teste"
    }
  }
}
```
## Phase Data Format
These are the fields of the phase and it's constrains:
- phaseId: Id of the Phase given by de Database.
  - Integer
  - Ignored on Create, mandatory on the other methods
- phaseName: Name of the Phase.
  - Integer
  - Mandatory
- phaseCode: Code of the phase.
  - String (Up to 50 chars)
  - Mandatory
- inputProducts: Array of the Input Products.
  - List of Product Objects
  - Ignored on Create, mandatory on the other methods
- outputProducts: Array of the Output Products.
  - List of Product Objects
  - Ignored on Create, mandatory on the other methods
- phaseParameters: Parameters of the phase.
  -List of Parameter Objects
  - Ignored on Create, mandatory on the other methods,
- predecessorPhaseId: Id of the previous phase
  - Integer
  - Optional
- sucessorPhasesIds: Id of the phases to be called.
  - Array Integer
  - Optional

### JSON Example:
```json
{
  "phaseId": 1,
  "phaseName": "cois2a",
  "phaseCode": "xpto",
  "inputProducts": [],
  "outputProducts": [],
  "phaseParameters": [],
  "predecessorPhaseId": 0,
  "sucessorPhasesId": [3,2],
}
```
## URLs
- api/phases/{optional=startat}{optional=quantity}
  - Get: Return List of Phaes
    - startat: represent where the list starts t the database (Default=0)
    - quantity: number of resuls in the query (Default=50)
  - Post: Create the Phase with the JSON in the body
    - Body: Phase JSON

- api/phases/{id}
  - Get: Return Phase with phaseId = ID
  - Put: Update the Phase with the JSON in the body with phaseId = ID
    - Body: Phase JSON
  - Delete: Delete the phase from the Database with phaseId = ID

- api/phases/products/output/{phaseid}
  - Get: Return All the Output Products with phaseId = phaseId
  - Post: Add the Product as an output product on phaseId
      - Body: Product JSON
  - Delete: Delete the product from the output product list

- api/phases/products/input/{phaseid}
  - Get: Return All the input Products with phaseId = phaseId
  - Post: Add the Product as an input product on phaseId
    - Body: Product JSON
  - Delete: Delete the product from the input product list

- api/phases/parameters/input/{phaseid}
  - Get: Return All the parameters with phaseId = phaseId
  - Post: Add the parameters parameter on phaseId = phaseId
    - Body: Product JSON
  - Delete: Delete the parameter from the parameter list