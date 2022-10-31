namespace Graph;

public static class VerticesPropertiesGraphExtension
{
    public static bool AreIncidentalVertices(this Graph graph, IList<uint> vertices) =>
        graph.GetData() is not null
            ? !vertices.Where((current, index) => !graph[(int)(current - 1), (int)vertices[index + 1] - 1]).Any()
            : throw new NullReferenceException("_matrix is null");

    public static bool IsClosedVertices(IList<uint> vertices) =>
        Equals(vertices[0], vertices[^1]);

    public static bool IsChain(this Graph graph, IList<uint> vertices)
    {
        if (!AreIncidentalVertices(graph, vertices))
            return false;

        var edges = vertices.Select((current, index) => new Tuple<uint, uint>(current, vertices[index + 1])).ToList();

        for (var i = 0; i < edges.Count - 1; i++)
        {
            for (var j = i + 1; j < edges.Count; j++)
            {
                var reverseTuple = new Tuple<uint, uint>(edges[j].Item2, edges[j].Item2);
                if (Equals(edges[i], edges[j]) || Equals(edges[i], reverseTuple))
                    return false;
            }
        }

        return true;
    }

    public static bool IsSimpleChain(this Graph graph, IList<uint> vertices)
    {
        if (!AreIncidentalVertices(graph, vertices))
            return false;

        for (var i = 1; i < vertices.Count - 1; i++)
        {
            for (var j = i + 1; j < vertices.Count - 1; j++)
            {
                if (vertices[i] == vertices[j])
                    return false;
            }
        }

        return true;
    }

    public static bool IsCycle(this Graph graph, IList<uint> vertices) =>
        IsClosedVertices(vertices) && IsChain(graph, vertices);

    public static bool IsSimpleCycle(this Graph graph, IList<uint> vertices) =>
        IsClosedVertices(vertices) && IsSimpleChain(graph, vertices);
}