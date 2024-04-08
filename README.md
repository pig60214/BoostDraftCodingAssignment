# Pull request description-like document
## What?
An XML determination function.
Rules:
- Only one root tag is needed
  - `<Design>Code</Design>`: ⭕ 
  - `apply`: ❌, no root tag
  - `<Design>Code</Design><Design>Code</Design>`: ❌, two root tag
- Opening tags and closing tags should be paired
  - `<Design>Code</Design>`: ⭕
  - `<Design><Code>Hello World</Code><Code>C#</Code></Design>`: ⭕
  - `<Design>Code<Design>`: ❌, need closing tag
 
## How?
Main point is a stack. If the next tag is the partner of the top of the stack, pop the top of the stack. Otherwise, push the tag onto the stack.

```D
bool DetermineXml(string xml) {
  var stack = new Stack();

  while(xml is not null or empty) {
    var tag = GetTag(xml);
    if (IsPair(stack.peek(), tag)) stack.pop()
    else stack.push(tag)
  }

  return stack.Count == 0
}
```

## Anything Else?
Implementation process: https://github.com/pig60214/BoostDraftCodingAssignment
