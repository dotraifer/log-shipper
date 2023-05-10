# log-shipper

This is a log shipper project - it is meant to collect logs, parse them, filler and send to outer db and sources

## Conf.yaml

Conf.yaml is the log shipper configuration file, it determineits the way your log shipper will act.
the log shipper contains 4 diffrent stages - called pipelines: Input, Parser, Filter, Output.
there can be multiple apperences from each pipeline, which called plugins.

### Input

This pipeline is responsible for collecting the logs from the diffrent sources, such as log files etc...

#### Tail

This is the plugin collecting the logs 
