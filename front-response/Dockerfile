# Étape 1: Dépendances
FROM node:20-alpine AS deps
WORKDIR /app

# Copie les fichiers de dépendances
COPY package.json package-lock.json* ./
RUN npm ci

# Étape 2: Build
FROM node:20-alpine AS builder
WORKDIR /app

# Copie les dépendances depuis l'étape précédente
COPY --from=deps /app/node_modules ./node_modules
COPY . .

# Build l'application
ENV NEXT_TELEMETRY_DISABLED 1
RUN npm run build

# Étape 3: Production
FROM node:20-alpine AS runner
WORKDIR /app

ENV NODE_ENV production
ENV NEXT_TELEMETRY_DISABLED 1

# Crée un utilisateur non-root
RUN addgroup --system --gid 1001 nodejs
RUN adduser --system --uid 1001 nextjs

# Copie les fichiers nécessaires
COPY --from=builder /app/public ./public
COPY --from=builder /app/.next/standalone ./
COPY --from=builder /app/.next/static ./.next/static

# Change le propriétaire
RUN chown -R nextjs:nodejs /app

USER nextjs

EXPOSE 3000

ENV PORT 3000
ENV HOSTNAME "0.0.0.0"

CMD ["node", "server.js"]