# PhaseAPI
API to Manage Phases on Lorien. Used to create, update, read and delete Phases. Also responsible for managing its products and Parameters

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
  - Get: Return List of Phases
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

- api/phases/parameters/{phaseid}
  - Get: Return All the parameters with phaseId = phaseId
  - Post: Add the parameters parameter on phaseId = phaseId
    - Body: Product JSON
  - Delete: Delete the parameter from the parameter list

# GatewayAPI
API Responsible to provide access to information nedeed to compose the recipe from other APIs

## URLs
- gateway/thinggroups/{optional=startat}{optional=quantity}
  - Get: Return List of Groups of Things
    - startat: represent where the list starts t the database (Default=0)
    - quantity: number of resuls in the query (Default=50)

- gateway/thinggroups/{id}
  - Get: Return Group of Things with thingGroupId = ID

- gateway/thinggroups/attachedthings/{groupid}
  - Get: List of Thing inside the group where thingGroupId = ID

- gateway/things/{id} 
  - Get: Thing where thingId = ID

- gateway/parameters/{optional=startat}{optional=quantity}
  - Get: Return List of Parameters
    - startat: represent where the list starts t the database (Default=0)
    - quantity: number of resuls in the query (Default=50)

- gateway/parameters/{id} 
  - Get: Parameter where parameterId = ID

- gateway/products/{optional=startat}{optional=quantity}
  - Get: Return List of Products
    - startat: represent where the list starts t the database (Default=0)
    - quantity: number of resuls in the query (Default=50)

- gateway/products/{id}
  - Get: Return Products with productId = ID

- gateway/products/childrenproducts/{productid}
  - Get: List of Products inside the product where productId = ID

# PhaseAPI
API to Manage Recipes on Lorien. Used to create, update, read and delete Recipes. Also responsible for managing its products and phases

## Phase Product Data Format
These are the fields of the phase product and it's constrains:
- recipeId: Id of the Recipe  given by de Database.
  - Integer
  - Ignored on Create, mandatory on the other methods
- recipeName: Name of the Recipe
  - String (Up to 50 chars)
  - Mandatory
- recipeCode: Code of the Recipe
  - String (Up to 50 chars)
  - Mandatory
- recipeProduct: Product Generated by the Recipe
  - Product JSON
  - Ignored on Create
- phasesId: ID of the phases used in this recipe
  - Array Integer
  - Ignored on Create, mandatory on the other methods  

### JSON Example:
```json
{
  "recipeId": 3,
  "recipeName": "teste de receita",
  "recipeCode": "5050505050",
  "recipeProduct": 
  {
    "phaseProductId": 13,
    "productId": 1,
    "value": "50",
    "measurementUnit": "kg",
    "product": 
    {
      "productId": 1,
      "producName": "Nome Teste 2",
      "producCode": "TesteCode",
      "productGTIN": "+9999999",
      "childrenProductsIds": []
    }
  },
  "phasesId": []
}
```

## URLs
- api/recipes/{optional=startat}{optional=quantity}
  - Get: Return List of Recipes
    - startat: represent where the list starts t the database (Default=0)
    - quantity: number of resuls in the query (Default=50)
  - Post: Create the Recipe with the JSON in the body
    - Recipe: Phase JSON

- api/recipes/{id}
  - Get: Return Phase with recipeId = ID
  - Put: Update the Recipe with the JSON in the body with recipeId = ID
    - Body: Recipe JSON
  - Delete: Delete the Recipe from the Database with recipeId = ID

- api/phases/product/{recipeId}
  - Get: Return the Product with recipeId = phaseId
  - Post: Add the Product as the product of the phase
      - Body: Product JSON
  - Delete: Delete the product from the Phase

- api/phases/phases/{recipeId}
  - Get: Return All the Phases with recipeId = recipeId
  - Post: Add the Phase on recipeId = recipeId
    - Body: Phase JSON
  - Delete: Delete the Phase from the phases list

# ProductAPI
API to Manage Products on Lorien. Used to create, update, read and delete Products. Also responsible for managing its children.

## Product Data Format
These are the fields of the thing and it's constrains:
- productId: Id of the Product given by de Database.
  - Integer
  - Ignored on Create, mandatory on the other methods
- parentProducId: Id of the Product wich this thing belong to.
  - Integer
  -  Ignored on Create and Update
- productName: Name of the Product given by the user.
  - String (Up to 50 chars)
  - Mandatory
- producDescription: Free description of the Producthing.
  - String (Up to 100 chars)
  - Optional
- productGTIN: GTIN of the product according to GS1.
  - String (Up to 50 chars)
  - Optional
- enabled: Products cannot be deleted they are just disabled in the backend and dont show up in the queries.
  - Boolean
  - Mandatory
- producCode: Code that might be used by the end user to identify the Product easily.
  - String (Up to 100 chars)
  - Optional
- childrenProductsIds: List of Id of Products from which this one is parent.
  - Array Integer
  - Ignored on Create and Update
- additionalInformation: List of additional information related to de product.
  - Array AdditionalInformation
  - Optional
  
### JSON Example:
```json
{
    "productId": 2,
    "parentProducId": null,
    "producName": "Nome Teste 2",
    "producDescription": "Teste Decription",
    "producCode": "TesteCode",
    "productGTIN": "+9999999",
    "childrenProductsIds": [1,2],
    "enabled": true,
    "additionalInformation": [
    {
      "information": "Densidade",
      "value": "60"
    }]
 }
```

## URLs
- api/products/{optional=startat}{optional=quantity}
  - Get: Return List of Products
    - startat: represent where the list starts t the database (Default=0)
    - quantity: number of resuls in the query (Default=50)
  - Post: Create the Product with the JSON in the body
    - Body: Product JSON

- api/products/{id}
  - Get: Return Product with productId = ID
  - Put: Update the Product with the JSON in the body with productId = ID
    - Body: Product JSON
  - Delete: Disable Product with productId = ID

- api/products/list{productid}{productid}
    - Get: Return List of Products with productid = ID

- api/products/childrenproducts/{parentId}
  - Get: Return List of Products which the parent is parentId

# ExtraAttibuteTypesAPI
API to Manage Extra Attibute Types that can be added on Lorien's products. Used to create, update, read and delete Extra Attibute Types.

## Extra Attibute Type Data Format
These are the fields of the thing and it's constrains:
- extraAttibruteTypeId: Id of the Extra Attibute Types given by de Database.
  - Integer
  - Ignored on Create, mandatory on the other methods
- extraAttibruteTypeName: Name of the Extra Attibute Types given by the user.
  - String (Up to 50 chars)
  - Mandatory
  
### JSON Example:
```json
{
  "extraAttibruteTypeId": 4,
  "extraAttibruteTypeName": "teste 2212"
}
```

## URLs
- api/extraattributetypes/
  - Get: Return List of Extra Attibute Types   
  - Post: Create the Extra Attibute Types with the JSON in the body
    - Body: Extra Attibute Types JSON

- api/extraattributetypes/{id}
  - Get: Return Extra Attibute Types with extraAttibruteTypeId = ID
  - Put: Update the Extra Attibute Types with the JSON in the body with extraAttibruteTypeId = ID
    - Body: Extra Attibute Types JSON
  - Delete: Disable Extra Attibute Types with extraAttibruteTypeId = ID