﻿namespace BlogDomain.Requests;

public class BlogUpdateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
}