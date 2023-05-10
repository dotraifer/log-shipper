# log-shipper

This is a log shipper project - it is meant to collect logs, parse them, filler and send to outer db and sources

## Conf.yaml

Conf.yaml is the log shipper configuration file, it determineits the way your log shipper will act.
the log shipper contains 4 diffrent stages - called pipelines: Input, Parser, Filter, Output.
there can be multiple apperences from each pipeline, which called plugins.

```YAML
input: 
    - type: ...

parser:
    - type: ...

filter:
    - type: ...
    
output:
    - type: ...
    
```

The Conf.yaml can also contain enviroment variables in the syntex 
`${enviroment variable name}`

### Input

This pipeline is responsible for collecting the logs from the diffrent sources, such as log files etc...

#### Tail

This is the plugin collecting the logs from a log file.

```YAML
    - type: tail
      path: C:\Users\dotan\log-shipper-test\log.txt
      tag: vm.${HOSTNAME}
```

### Parser

This pipeline is responsible for parsing the logs

#### Regex

will parse the log to fields using c# regex

```YAML
    - type: regex
      match: vm.*
      regex: '^(?<timestamp>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3})\s+(?<severity>\w+):\s+(?<message>.*)$'
```

### Filter

This pipeline is responsible for Adjusting the logs

#### Modify

will modify the log - add, set, remove etc, to the log data.

```YAML
    - type: modify
      match: vm.*
      add: 
        name: dotan
        age: dotan
      set:
        host: vm.${HOSTNAME}
      remove:
        - parser
```

### Output

This pipeline is responsible for sending the logs to outer source

#### Stdout

```YAML
    - type: stdout 
```

## Routing

By define **tag** in the input section, we can choose the exact course the log will pass. Every filter, Parser or Output can contain the **match** label.
only the logs with the Tag matching the match in those pipelines will enter this pipeline.
You can also use * as wild card.
The defult Tag and match are * .
 
