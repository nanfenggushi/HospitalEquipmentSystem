import http from "node:http";
import https from "node:https";

const UPSTREAM = "https://api.juzentgf.com";
const UPSTREAM_PATH = "/v1/yunshen/chat";
const API_KEY = "UCarFXp1NymSn96vjLWXEK3AL2nZpZgGN9w1KC8yT0M";
const PORT = 15730;

const HOP_BY_HOP = new Set([
  "connection", "keep-alive", "proxy-authenticate", "proxy-authorization",
  "te", "trailer", "transfer-encoding", "upgrade",
]);

const server = http.createServer((req, res) => {
  const url = new URL(req.url, `http://${req.headers.host || "127.0.0.1"}`);

  if (req.method === "GET" && url.pathname === "/v1/models") {
    res.writeHead(200, { "Content-Type": "application/json" });
    res.end(JSON.stringify({
      object: "list",
      data: [{ id: "yunshen", object: "model", owned_by: "juzentgf" }],
    }));
    return;
  }

  if (req.method !== "POST" || url.pathname !== "/v1/chat/completions") {
    res.writeHead(404, { "Content-Type": "application/json" });
    res.end(JSON.stringify({ error: { message: "not found" } }));
    return;
  }

  const chunks = [];
  req.on("data", (c) => chunks.push(c));
  req.on("end", () => {
    const body = Buffer.concat(chunks);
    const upstreamReq = https.request(
      UPSTREAM + UPSTREAM_PATH,
      {
        method: "POST",
        headers: {
          "Content-Type": req.headers["content-type"] || "application/json",
          Authorization: `Bearer ${API_KEY}`,
          "Content-Length": body.length,
        },
      },
      (upstreamRes) => {
        const headers = {};
        for (const [k, v] of Object.entries(upstreamRes.headers)) {
          if (!HOP_BY_HOP.has(k)) headers[k] = v;
        }
        res.writeHead(upstreamRes.statusCode || 500, headers);
        upstreamRes.pipe(res);
      }
    );
    upstreamReq.on("error", (err) => {
      res.writeHead(502, { "Content-Type": "application/json" });
      res.end(JSON.stringify({ error: { message: String(err) } }));
    });
    upstreamReq.end(body);
  });
});

server.listen(PORT, "127.0.0.1", () => {
  console.log(`juzentgf proxy: http://127.0.0.1:${PORT}/v1 -> ${UPSTREAM}${UPSTREAM_PATH}`);
});
